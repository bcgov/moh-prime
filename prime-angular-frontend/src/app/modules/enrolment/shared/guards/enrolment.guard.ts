import { Injectable, Inject } from '@angular/core';
import { Router } from '@angular/router';

import { Observable, of, from } from 'rxjs';
import { map, exhaustMap } from 'rxjs/operators';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { BaseGuard } from '@core/guards/base.guard';
import { LoggerService } from '@core/services/logger.service';
import { Enrolment } from '@shared/models/enrolment.model';
import { EnrolmentStatus } from '@shared/enums/enrolment-status.enum';
import { Enrollee } from '@shared/models/enrollee.model';
import { AuthService } from '@auth/shared/services/auth.service';
import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { User } from '@auth/shared/models/user.model';

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
        exhaustMap(({ userId, firstName, lastName, dateOfBirth, physicalAddress }: User) => {
          // Enforced the enrolment type instead of using Partial<Enrolment>
          // to avoid creating constructors and partials for every model
          const enrollee = {
            userId,
            firstName,
            lastName,
            dateOfBirth,
            physicalAddress,
            voicePhone: null,
            contactEmail: null
          } as Enrollee;

          return this.enrolmentResource.createEnrollee(enrollee);
        })
      );

    return this.enrolmentResource.enrollee()
      .pipe(
        exhaustMap((enrolment: Enrolment) => {
          return (!enrolment)
            ? createEnrollee$
            : of(enrolment);
        }),
        map((enrolment: Enrolment) => {
          // Store the enrolment for access throughout enrolment
          this.enrolmentService.enrolment$.next(enrolment);
          return this.routeDestination(routePath, enrolment);
        })
      );
  }

  /**
   * @description
   * Determine the route destination based on the enrolment status.
   */
  private routeDestination(routePath: string, enrolment: Enrolment) {
    // On login the enrollees will always be redirected to
    // the collection notice
    if (routePath.includes(EnrolmentRoutes.COLLECTION_NOTICE)) {
      return true;
    }

    // Otherwise, routes are directed based on enrolment status
    if (!enrolment) {
      return this.navigate(routePath, EnrolmentRoutes.DEMOGRAPHIC);
    } else if (enrolment) {
      switch (enrolment.currentStatus.status.code) {
        case EnrolmentStatus.IN_PROGRESS:
          return this.manageInProgressRouting(routePath, enrolment);
        case EnrolmentStatus.SUBMITTED:
          return this.navigate(routePath, EnrolmentRoutes.SUBMISSION_CONFIRMATION);
        case EnrolmentStatus.ADJUDICATED_APPROVED:
          return this.manageApprovedRouting(routePath, enrolment);
        case EnrolmentStatus.DECLINED:
          return this.navigate(routePath, EnrolmentRoutes.DECLINED);
        case EnrolmentStatus.ACCEPTED_TOS:
          return this.manageAcceptedToaRouting(routePath, enrolment);
        case EnrolmentStatus.DECLINED_TOS:
          return this.navigate(routePath, EnrolmentRoutes.DECLINED_TERMS_OF_ACCESS);
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
  private manageInProgressRouting(routePath: string, enrolment: Enrolment) {
    const enrolmentSubmissionRoutes = [
      ...EnrolmentRoutes.enrolmentSubmissionRoutes()
    ];
    const route = routePath.split('/').pop();

    const redirectionRoute = (!enrolment.profileCompleted)
      ? EnrolmentRoutes.DEMOGRAPHIC // Only for new enrolments with incomplete profiles
      : EnrolmentRoutes.OVERVIEW;

    if (
      !enrolment.profileCompleted &&
      [...enrolmentSubmissionRoutes, EnrolmentRoutes.OVERVIEW].includes(route)
    ) {
      this.navigate(routePath, redirectionRoute);
    }

    const hasNotCompletedProfile = !enrolment.profileCompleted && route === EnrolmentRoutes.OVERVIEW;
    const hasNotSubmittedEnrolment = enrolmentSubmissionRoutes.includes(route);

    return (hasNotCompletedProfile || hasNotSubmittedEnrolment)
      // Prevent access to post enrolment routes
      ? this.navigate(routePath, redirectionRoute)
      // Otherwise, allow the route to resolve
      : true;
  }

  private manageApprovedRouting(routePath: string, enrolment: Enrolment) {
    const whiteListedRoutes = [
      EnrolmentRoutes.TERMS_OF_ACCESS,
      EnrolmentRoutes.PHARMANET_TRANSACTIONS,
      EnrolmentRoutes.CURRENT_ACCESS_TERM
    ];
    const route = routePath.split('/').pop();

    if (!whiteListedRoutes.includes(route)) {
      return this.navigate(routePath, EnrolmentRoutes.TERMS_OF_ACCESS);
    }

    return true;
  }

  /**
   * @description
   * Manages the routing for enrollees
   */
  private manageAcceptedToaRouting(routePath: string, enrolment: Enrolment) {
    const enrolmentSubmissionRoutes = [
      ...EnrolmentRoutes.enrolmentSubmissionRoutes()
    ];

    if (enrolmentSubmissionRoutes.includes(routePath)) {
      this.navigate(routePath, EnrolmentRoutes.OVERVIEW);
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
