import { Injectable, Inject } from '@angular/core';
import { Router } from '@angular/router';

import { Observable } from 'rxjs';
import { exhaustMap, map, tap, pairwise } from 'rxjs/operators';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { BaseGuard } from '@core/guards/base.guard';
import { ConsoleLoggerService } from '@core/services/console-logger.service';
import { Enrolment } from '@shared/models/enrolment.model';
import { Enrollee } from '@shared/models/enrollee.model';
import { BcscUser } from '@auth/shared/models/bcsc-user.model';
import { EnrolmentStatusEnum } from '@shared/enums/enrolment-status.enum';

import { AuthService } from '@auth/shared/services/auth.service';
import { IdentityProviderEnum } from '@auth/shared/enum/identity-provider.enum';
import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { EnrolmentFormStateService } from '@enrolment/shared/services/enrolment-form-state.service';

@Injectable({
  providedIn: 'root'
})
export class EnrolmentGuard extends BaseGuard {
  public isPotentialPaperEnrolleeReturnee: boolean;

  constructor(
    protected authService: AuthService,
    protected logger: ConsoleLoggerService,
    @Inject(APP_CONFIG) private config: AppConfig,
    private enrolmentResource: EnrolmentResource,
    private enrolmentService: EnrolmentService,
    private enrolmentFormStateService: EnrolmentFormStateService,
    private router: Router
  ) {
    super(authService, logger);
  }

  /**
   * @description
   * Check an enrollee enrolment status, and attempt to redirect
   * to an appropriate destination based on its existence or
   * status, as well as, their authentication provider.
   */
  protected checkAccess(routePath: string = null): Observable<boolean> | Promise<boolean> {
    return this.enrolmentResource.enrollee()
      .pipe(
        tap((enrolment: Enrolment) => {
          // Store the enrolment for access throughout enrolment, which will
          // allows be the most up-to-date enrolment (source of truth)
          this.enrolmentService.enrolment$.next(enrolment);
        }),
        exhaustMap((enrolment: Enrolment) =>
          this.authService.identityProvider$()
            .pipe(map((identityProvider: IdentityProviderEnum) => [routePath, enrolment, identityProvider]))
        ),
        map((params: [string, Enrolment, IdentityProviderEnum]) =>
          this.routeDestination(...params)
        ),
      );
  }

  /**
   * @description
   * Determine the route destination based on the enrolment status.
   */
  private routeDestination(routePath: string, enrolment: Enrolment, identityProvider: IdentityProviderEnum): boolean {
    // On login the enrollees will always be redirected to
    // the collection notice, which should always resolve
    if (routePath.includes(EnrolmentRoutes.COLLECTION_NOTICE)) {
      return true;
    }

    // If enrollee date of birth matches a paper enrollee date of birth, go to the
    // NOBCSC page
    // if (routePath.includes(EnrolmentRoutes.PAPER_ENROLLEE_RETURNEE_DECLARATION)) {

    // }

    if (!enrolment) {
      // Route based on identity provider to determine sequence of routing
      // required to create a new enrolment
      return this.identityProviderRouting(routePath, enrolment, identityProvider);
    }

    routePath = this.checkBlacklistedRoutes(routePath, identityProvider);

    // Otherwise, routes are dictated based on enrolment status
    return this.enrolmentStatusRouting(routePath, enrolment, identityProvider);
  }

  /**
   * @description
   * Determine routing by identity provider for new enrolments that have not
   * had their enrolment created.
   */
  private identityProviderRouting(routePath: string, enrolment: Enrolment, identityProvider: IdentityProviderEnum): boolean {
    switch (identityProvider) {
      case IdentityProviderEnum.BCEID:
        return this.manageBceidRouting(routePath);
      case IdentityProviderEnum.BCSC:
        return this.navigate(routePath, EnrolmentRoutes.BCSC_DEMOGRAPHIC);
      default:
        return false; // Identity provider is unknown and routing cannot be determined
    }
  }

  /**
   * @description
   * Step by step routing sequence for initial enrolment with BCeID.
   */
  private manageBceidRouting(routePath: string): boolean {
    const currentRoutePath = RouteUtils.currentRoutePath(this.router.url);
    const nextRoutePath = RouteUtils.currentRoutePath(routePath);

    if (
      currentRoutePath === EnrolmentRoutes.ACCESS_CODE &&
      nextRoutePath === EnrolmentRoutes.ID_SUBMISSION
    ) {
      return this.navigate(routePath, EnrolmentRoutes.ID_SUBMISSION);
    } else if (
      currentRoutePath === EnrolmentRoutes.ID_SUBMISSION &&
      nextRoutePath === EnrolmentRoutes.BCEID_DEMOGRAPHIC
    ) {
      return this.navigate(routePath, EnrolmentRoutes.BCEID_DEMOGRAPHIC);
    } else {
      // Otherwise, start at the beginning of the enrolment process
      return this.navigate(routePath, EnrolmentRoutes.ACCESS_CODE);
    }
  }

  /**
   * @description
   * Determine routing based on enrolment status.
   */
  private enrolmentStatusRouting(routePath: string, enrolment: Enrolment, identityProvider: IdentityProviderEnum): boolean {
    switch (enrolment.currentStatus.statusCode) {
      case EnrolmentStatusEnum.EDITABLE:
        return this.manageEditableRouting(routePath, enrolment, identityProvider);
      case EnrolmentStatusEnum.UNDER_REVIEW:
        return this.manageUnderReviewRouting(routePath, enrolment);
      case EnrolmentStatusEnum.REQUIRES_TOA:
        return this.manageRequiresToaRouting(routePath, enrolment);
      case EnrolmentStatusEnum.LOCKED:
        return this.navigate(routePath, EnrolmentRoutes.ACCESS_LOCKED);
      case EnrolmentStatusEnum.DECLINED:
        return this.navigate(routePath, EnrolmentRoutes.ACCESS_DECLINED);
      default:
        return false; // Status is unknown and routing cannot be determined
    }
  }

  /**
   * @description
   * Manages the routing for enrollees that are in-progress filling
   * out their initial enrolment, which prevents access to
   * post-enrolment routes.
   */
  private manageEditableRouting(routePath: string, enrolment: Enrolment, identityProvider: IdentityProviderEnum): boolean {
    const hasNotCompletedProfile = !enrolment.profileCompleted;

    const route = this.routePath(routePath);
    const redirectionRoute = (hasNotCompletedProfile)
      ? (identityProvider === IdentityProviderEnum.BCEID)
        ? EnrolmentRoutes.BCEID_DEMOGRAPHIC
        : EnrolmentRoutes.BCSC_DEMOGRAPHIC
      : EnrolmentRoutes.OVERVIEW;

    const blacklistedRoutes = [
      ...EnrolmentRoutes.enrolmentSubmissionRoutes()
    ];

    if (hasNotCompletedProfile) {
      // No access to overview if you've not completed the wizard
      blacklistedRoutes.push(EnrolmentRoutes.OVERVIEW);
    }

    let certifications = enrolment.certifications;
    let careSettings = enrolment.careSettings;

    // When renewing an enrollee may have updates that allow or
    // prevent routing to specific views, which should be
    if (this.enrolmentFormStateService.isPatched && this.enrolmentFormStateService.isDirty) {
      certifications = this.enrolmentFormStateService.regulatoryFormState.collegeCertifications;
      careSettings = this.enrolmentFormStateService.careSettingsForm.get('careSettings').value;
    }

    if (!this.enrolmentService.canRequestRemoteAccess(certifications, careSettings)) {
      // No access to remote access if OBO or pharmacist
      blacklistedRoutes.push(EnrolmentRoutes.REMOTE_ACCESS);
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
    const route = this.routePath(routePath);
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

  /**
   * @description
   * General blacklisted routes based on enrolment existence and provider.
   */
  private checkBlacklistedRoutes(routePath: string, identityProvider: IdentityProviderEnum): string {
    // Blacklisted routes if an enrolment exists regardless of provider
    if (
      [
        EnrolmentRoutes.ACCESS_CODE,
        EnrolmentRoutes.ID_SUBMISSION
      ].includes(RouteUtils.currentRoutePath(routePath))
    ) {
      return routePath.replace(
        new RegExp(`${EnrolmentRoutes.ACCESS_CODE}|${EnrolmentRoutes.ID_SUBMISSION}`),
        EnrolmentRoutes.OVERVIEW
      );
    }

    // Blacklisted routes based on provider
    if (routePath.includes(EnrolmentRoutes.BCSC_DEMOGRAPHIC) && identityProvider === IdentityProviderEnum.BCEID) {
      return routePath.replace(
        EnrolmentRoutes.BCSC_DEMOGRAPHIC,
        EnrolmentRoutes.OVERVIEW
      );
    } else if (routePath.includes(EnrolmentRoutes.BCEID_DEMOGRAPHIC) && identityProvider === IdentityProviderEnum.BCSC) {
      return routePath.replace(
        EnrolmentRoutes.BCEID_DEMOGRAPHIC,
        EnrolmentRoutes.OVERVIEW
      );
    }

    return routePath;
  }


}
