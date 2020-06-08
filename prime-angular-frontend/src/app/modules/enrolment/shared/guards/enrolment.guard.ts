import { Injectable, Inject } from '@angular/core';
import { Router } from '@angular/router';

import { Observable, of, from } from 'rxjs';
import { map, exhaustMap } from 'rxjs/operators';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { BaseGuard } from '@core/guards/base.guard';
import { LoggerService } from '@core/services/logger.service';
import { Enrolment } from '@shared/models/enrolment.model';
import { EnrolmentStatus } from '@shared/enums/enrolment-status.enum';
import { User } from '@auth/shared/models/user.model';
import { Enrollee } from '@shared/models/enrollee.model';
import { AuthService } from '@auth/shared/services/auth.service';
import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';

@Injectable({
  providedIn: 'root'
})
export class EnrolmentGuard extends BaseGuard {
  constructor(
    protected authService: AuthService,
    protected logger: LoggerService,
    @Inject(APP_CONFIG) private config: AppConfig,
    private enrolmentResource: EnrolmentResource,
    private enrolmentService: EnrolmentService,
    private router: Router
  ) {
    super(authService, logger);
  }

  /**
   * @description
   * Check an enrollee enrolment status, and attempt to redirect
   * to an appropriate destination based on its existence or
   * status.
   */
  protected checkAccess(routePath: string = null): Observable<boolean> | Promise<boolean> {
    const user$ = from(this.authService.getUser());
    const createEnrollee$ = user$
      .pipe(
        exhaustMap(({ userId, hpdid, firstName, lastName, dateOfBirth, physicalAddress }: User) => {
          // Enforced the enrolment type instead of using Partial<Enrolment>
          // to avoid creating constructors and partials for every model
          const enrollee = {
            // Providing only the minimum required fields for creating an enrollee
            userId,
            hpdid,
            firstName,
            lastName,
            dateOfBirth,
            physicalAddress,
            voicePhone: null,
            contactEmail: null
          } as Enrollee;

          return this.enrolmentResource.createEnrollee(enrollee);
        }),
        map((enrolment: Enrolment) => [enrolment, true])
      );

    return this.enrolmentResource.enrollee()
      .pipe(
        exhaustMap((enrolment: Enrolment) =>
          (enrolment)
            ? of([enrolment, false])
            : createEnrollee$
        ),
        map(([enrolment, isNewEnrolment]: [Enrolment, boolean]) => {
          // Store the enrolment for access throughout enrolment, which
          // will allows be the most up-to-date enrolment
          this.enrolmentService.enrolment$.next(enrolment);
          return this.routeDestination(routePath, enrolment, isNewEnrolment);
        })
      );
  }

  /**
   * @description
   * Determine the route destination based on the enrolment status.
   */
  private routeDestination(routePath: string, enrolment: Enrolment, isNewEnrolment: boolean = false) {
    // On login the enrollees will always be redirected to
    // the collection notice
    if (routePath.includes(EnrolmentRoutes.COLLECTION_NOTICE)) {
      return true;
    } else if (isNewEnrolment) {
      // TODO needs to be refactored, why would a new enrolment go to OVERVIEW?
      this.navigate(routePath, EnrolmentRoutes.OVERVIEW);
    }

    // Otherwise, routes are directed based on enrolment status
    //  TODO should never happen and should redirect to an error if it does
    if (!enrolment) {
      return this.navigate(routePath, EnrolmentRoutes.DEMOGRAPHIC);
    } else if (enrolment) {
      switch (enrolment.currentStatus.statusCode) {
        case EnrolmentStatus.EDITABLE:
          return this.manageEditableRouting(routePath, enrolment);
        case EnrolmentStatus.UNDER_REVIEW:
          return this.manageUnderReviewRouting(routePath, enrolment);
        case EnrolmentStatus.REQUIRES_TOA:
          return this.manageRequiresToaRouting(routePath, enrolment);
        case EnrolmentStatus.LOCKED:
          return this.navigate(routePath, EnrolmentRoutes.ACCESS_LOCKED);
        case EnrolmentStatus.DECLINED:
          return this.navigate(routePath, EnrolmentRoutes.ACCESS_DECLINED);
      }
    }

    // Otherwise, prevent the route from resolving
    return false;
  }

  /**
   * @description
   * Manages the routing for enrollees that are in-progress filling
   * out their initial enrolment, which prevents access to
   * post-enrolment routes.
   */
  private manageEditableRouting(routePath: string, enrolment: Enrolment): boolean {
    const hasNotCompletedProfile = !enrolment.profileCompleted;

    const route = this.route(routePath);
    const redirectionRoute = (hasNotCompletedProfile)
      ? EnrolmentRoutes.DEMOGRAPHIC // Only for new enrolments with incomplete profiles
      : EnrolmentRoutes.OVERVIEW;
    const blacklistedRoutes = [
      ...EnrolmentRoutes.enrolmentSubmissionRoutes()
    ];

    if (hasNotCompletedProfile) {
      // No access to overview if you've not completed the wizard
      blacklistedRoutes.push(EnrolmentRoutes.OVERVIEW);
    }

    return (blacklistedRoutes.includes(route))
      // Prevent access to post enrolment/blacklisted routes
      ? this.navigate(routePath, redirectionRoute)
      // Otherwise, allow the route to resolve
      : true;
  }

  private manageUnderReviewRouting(routePath: string, enrolment: Enrolment): boolean {
    return this.manageRouting(routePath, EnrolmentRoutes.SUBMISSION_CONFIRMATION, enrolment);
  }

  private manageRequiresToaRouting(routePath: string, enrolment: Enrolment): boolean {
    return this.manageRouting(routePath, EnrolmentRoutes.PENDING_ACCESS_TERM, enrolment);
  }

  private manageRouting(routePath: string, defaultRoute: string, enrolment: Enrolment): boolean {
    const route = this.route(routePath);
    // Allow access to an extended set of routes if the enrollee
    // has accepted at least one TOA
    const whiteListedRoutes = (!!enrolment.expiryDate)
      ? [
        ...EnrolmentRoutes.enrolmentEditableRoutes(),
        // Allow read-only access to the enrollee profile
        EnrolmentRoutes.OVERVIEW
      ]
      : [];

    if (!whiteListedRoutes.includes(route)) {
      return this.navigate(routePath, defaultRoute);
    }

    return true;
  }

  /**
   * @description
   * Prevent infinite route loops by navigating to a route only
   * when the current route path is not the destination path.
   */
  private navigate(routePath: string, destinationPath: string): boolean {
    const enrolmentRoutePath = this.config.routes.enrolment;

    if (routePath === `/${enrolmentRoutePath}/${destinationPath}`) {
      return true;
    } else {
      this.router.navigate([enrolmentRoutePath, destinationPath]);
      return false;
    }
  }
}
