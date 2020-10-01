import { Injectable, Inject } from '@angular/core';
import { Router } from '@angular/router';

import { Observable } from 'rxjs';
import { exhaustMap, map, tap } from 'rxjs/operators';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { BaseGuard } from '@core/guards/base.guard';
import { LoggerService } from '@core/services/logger.service';
import { Enrolment } from '@shared/models/enrolment.model';
import { EnrolmentStatus } from '@shared/enums/enrolment-status.enum';
import { CollegeLicenceClass } from '@shared/enums/college-licence-class.enum';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';

import { AuthService } from '@auth/shared/services/auth.service';
import { IdentityProvider } from '@auth/shared/enum/identity-provider.enum';
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
            .pipe(map((identityProvider: IdentityProvider) => [routePath, enrolment, identityProvider]))
        ),
        map((params: [string, Enrolment, IdentityProvider]) =>
          this.routeDestination(...params)
        )
      );
  }

  /**
   * @description
   * Determine the route destination based on the enrolment status.
   */
  private routeDestination(routePath: string, enrolment: Enrolment, identityProvider: IdentityProvider): boolean {
    // On login the enrollees will always be redirected to
    // the collection notice
    if (routePath.includes(EnrolmentRoutes.COLLECTION_NOTICE)) {
      return true;
    }

    if (!enrolment) {
      // Route based on identity provider to determine sequence of routing
      // required to create a new enrolment
      return this.identityProviderRouting(routePath, enrolment, identityProvider);
    } else if (enrolment) {
      // Otherwise, routes are directed based on enrolment status
      return this.enrolmentStatusRouting(routePath, enrolment, identityProvider);
    }

    // Otherwise, prevent the route from resolving
    return false;
  }

  /**
   * @description
   * Determine routing by identity provider for new enrolments that have not
   * had their enrolment created.
   */
  private identityProviderRouting(routePath: string, enrolment: Enrolment, identityProvider: IdentityProvider): boolean {
    switch (identityProvider) {
      case IdentityProvider.BCEID:
        return this.manageBceidRouting(routePath, enrolment, identityProvider);
      case IdentityProvider.BCSC:
        return this.navigate(routePath, EnrolmentRoutes.DEMOGRAPHIC);
      default:
        return false; // Identity provider is unknown and routing cannot be determined
    }
  }

  private manageBceidRouting(routePath: string, enrolment: Enrolment, identityProvider: IdentityProvider): boolean {
    // const tree: UrlTree =router.parseUrl('/team/33/(user/victor//support:help)?debug=true#fragment');
    // const f = tree.fragment; // return 'fragment'
    // const q = tree.queryParams; // returns {debug: 'true'}
    // const g: UrlSegmentGroup = tree.root.children[PRIMARY_OUTLET];
    // const s: UrlSegment[] = g.segments; // returns 2 segments 'team' and '33'
    // g.children[PRIMARY_OUTLET].segments; // returns 2 segments 'user' and 'victor'
    // g.children['support'].segments; // return 1 segment 'help'

    console.log('ROUTER', this.router.url);
    console.log('ROUTER', this.router.parseUrl(this.router.url));

    // TODO a sequence of routing is required
    // TODO check previous and current route and whether sequence is correct
    // TODO Incorrect sequence goes back to access code

    return this.navigate(routePath, EnrolmentRoutes.IDENTITY_ACCESS_CODE);
  }

  /**
   * @description
   * Determine routing based on enrolment status.
   */
  private enrolmentStatusRouting(routePath: string, enrolment: Enrolment, identityProvider: IdentityProvider): boolean {
    switch (enrolment.currentStatus.statusCode) {
      case EnrolmentStatus.EDITABLE:
        return this.manageEditableRouting(routePath, enrolment, identityProvider);
      case EnrolmentStatus.UNDER_REVIEW:
        return this.manageUnderReviewRouting(routePath, enrolment);
      case EnrolmentStatus.REQUIRES_TOA:
        return this.manageRequiresToaRouting(routePath, enrolment);
      case EnrolmentStatus.LOCKED:
        return this.navigate(routePath, EnrolmentRoutes.ACCESS_LOCKED);
      case EnrolmentStatus.DECLINED:
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
  private manageEditableRouting(routePath: string, enrolment: Enrolment, identityProvider: IdentityProvider): boolean {
    const hasNotCompletedProfile = !enrolment.profileCompleted;

    const route = this.route(routePath);
    const redirectionRoute = (hasNotCompletedProfile)
      ? (identityProvider === IdentityProvider.BCEID)
        ? EnrolmentRoutes.IDENTITY_PROFILE
        : EnrolmentRoutes.DEMOGRAPHIC
      : EnrolmentRoutes.OVERVIEW;

    const blacklistedRoutes = [
      ...EnrolmentRoutes.enrolmentSubmissionRoutes()
    ];

    if (hasNotCompletedProfile) {
      // No access to overview if you've not completed the wizard
      blacklistedRoutes.push(EnrolmentRoutes.OVERVIEW);
    }

    if (!enrolment.certifications.length
      || enrolment.certifications.some(cert => cert.collegeCode === CollegeLicenceClass.CPBC)
      || enrolment.careSettings.some(cs => cs.careSettingCode === CareSettingEnum.COMMUNITY_PHARMACIST)) {
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
