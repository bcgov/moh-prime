import { Injectable, Inject } from '@angular/core';
import { Router } from '@angular/router';

import { Observable, of, pipe, UnaryFunction } from 'rxjs';
import { exhaustMap, map, tap } from 'rxjs/operators';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { BaseGuard } from '@core/guards/base.guard';
import { ConsoleLoggerService } from '@core/services/console-logger.service';
import { Enrolment } from '@shared/models/enrolment.model';
import { EnrolmentStatusEnum } from '@shared/enums/enrolment-status.enum';

import { AuthService } from '@auth/shared/services/auth.service';
import { IdentityProviderEnum } from '@auth/shared/enum/identity-provider.enum';
import { BcscUser } from '@auth/shared/models/bcsc-user.model';
import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { EnrolmentFormStateService } from '@enrolment/shared/services/enrolment-form-state.service';

@Injectable({
  providedIn: 'root'
})
export class EnrolmentGuard extends BaseGuard {

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
    return this.authService.getUser$()
      .pipe(
        exhaustMap((user: BcscUser) => this.enrolmentResource.enrollee(user.username)),
        tap((enrolment: Enrolment) => {
          // Store the enrolment for access throughout enrolment, which will
          // allows be the most up-to-date enrolment (source of truth)
          this.enrolmentService.enrolment$.next(enrolment);
        }),
        exhaustMap((enrolment: Enrolment) =>
          this.authService.getUser$()
            .pipe(map(({ dateOfBirth }: BcscUser) => [dateOfBirth, enrolment]))
        ),
        this.onInitialEnrolmentCheckForMatchingPaperEnrollee(),
        exhaustMap((enrolment: Enrolment) =>
          this.authService.identityProvider$()
            .pipe(map((identityProvider: IdentityProviderEnum) => [routePath, enrolment, identityProvider]))
        ),
        map((params: [string, Enrolment, IdentityProviderEnum]) =>
          this.routeDestination(...params)
        )
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

    if (
      this.enrolmentService.isMatchingPaperEnrollee &&
      routePath.includes(EnrolmentRoutes.PAPER_ENROLLEE_DECLARATION)
    ) {
      return this.navigate(routePath, EnrolmentRoutes.PAPER_ENROLLEE_DECLARATION);
    }

    if (!enrolment) {
      // Route based on identity provider to determine sequence of routing
      // required to create a new enrolment
      return this.identityProviderRouting(routePath, enrolment, identityProvider);
    }

    routePath = this.checkDeniedRoutes(routePath, identityProvider);

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
      case IdentityProviderEnum.BCSC_MOH:
        return this.navigate(routePath, EnrolmentRoutes.BCSC_DEMOGRAPHIC);
      default:
        this.logger.warn(`Unknown identityProvider:  ${identityProvider}`);
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

    const deniedRoutes = [
      ...EnrolmentRoutes.enrolmentSubmissionRoutes()
    ];

    if (hasNotCompletedProfile) {
      // No access to overview if you've not completed the wizard
      deniedRoutes.push(EnrolmentRoutes.OVERVIEW);
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
      deniedRoutes.push(EnrolmentRoutes.REMOTE_ACCESS);
    }

    if (enrolment?.currentTOAStatus === "") {
      deniedRoutes.push(EnrolmentRoutes.PHARMANET_ENROLMENT_SUMMARY)
    }

    return (deniedRoutes.includes(route))
      // Prevent access to post enrolment/denied routes
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
   * General denied routes based on enrolment existence and provider.
   */
  private checkDeniedRoutes(routePath: string, identityProvider: IdentityProviderEnum): string {
    // Denied routes if an enrolment exists regardless of provider
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

    // Denied routes based on provider
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

    // Denied routes based on matching paper enrolment
    if (
      // TODO would be better to add this route based on need instead of removing it since:
      //  1) 99.9% of enrollees aren't paper enrollees, and
      //  2) <1% of the enrolment lifecycle enrolments is the initial enrolment
      !this.enrolmentService.isInitialEnrolment ||
      (routePath.includes(EnrolmentRoutes.PAPER_ENROLLEE_DECLARATION) && !this.enrolmentService.isMatchingPaperEnrollee)
    ) {
      return routePath.replace(
        EnrolmentRoutes.PAPER_ENROLLEE_DECLARATION,
        EnrolmentRoutes.OVERVIEW
      );
    }

    return routePath;
  }

  /**
   * @description
   * Check for a matching paper enrollee on a new enrolment.
   */
  private onInitialEnrolmentCheckForMatchingPaperEnrollee(): UnaryFunction<Observable<[string, Enrolment]>, Observable<Enrolment>> {
    return pipe(
      exhaustMap(([dateOfBirth, enrolment]: [string, Enrolment]) =>
        (this.enrolmentService.isInitialEnrolment && dateOfBirth && this.enrolmentService.isMatchingPaperEnrollee === null && !enrolment?.approvedDate)
          ? this.enrolmentResource.checkForMatchingPaperSubmission(dateOfBirth)
            .pipe(
              map((isMatchingPaperEnrollee: boolean) => {
                this.enrolmentService.isMatchingPaperEnrollee = isMatchingPaperEnrollee;
                return enrolment;
              })
            )
          : of(enrolment)
      )
    );
  }
}
