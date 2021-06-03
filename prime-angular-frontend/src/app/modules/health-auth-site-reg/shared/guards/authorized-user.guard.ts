import { Inject, Injectable } from '@angular/core';
import { Params, Router } from '@angular/router';

import { Observable } from 'rxjs';
import { exhaustMap, map } from 'rxjs/operators';

import { BaseGuard } from '@core/guards/base.guard';
import { LoggerService } from '@core/services/logger.service';
import { HealthAuthorityResource } from '@core/resources/health-authority-resource.service';
import { AuthorizedUser } from '@shared/models/authorized-user.model';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { BcscUser } from '@auth/shared/models/bcsc-user.model';
import { AuthService } from '@auth/shared/services/auth.service';
import { AccessStatusEnum } from '@health-auth/shared/enums/access-status.enum';
import { HealthAuthSiteRegRoutes } from '@health-auth/health-auth-site-reg.routes';
import { AuthorizedUserService } from '@health-auth/shared/services/authorized-user.service';

/**
 * @description
 * Used to gate entry to routes based on the authorized user's
 * access status.
 */
@Injectable({
  providedIn: 'root'
})
export class AuthorizedUserGuard extends BaseGuard {
  constructor(
    protected authService: AuthService,
    protected logger: LoggerService,
    @Inject(APP_CONFIG) private config: AppConfig,
    private router: Router,
    private authorizedUserService: AuthorizedUserService,
    private healthAuthorityResource: HealthAuthorityResource
  ) {
    super(authService, logger);
  }

  protected checkAccess(routePath: string, params?: Params): Observable<boolean> | Promise<boolean> {
    return this.authService.getUser$()
      .pipe(
        // Having no authorized user in the same redirection logic for the user as
        // an unapproved status, and therefore not handled individually
        exhaustMap((user: BcscUser) =>
          this.healthAuthorityResource.getAuthorizedUserByUserId(user.userId)
        ),
        map((authorizedUser: AuthorizedUser | null) =>
          (authorizedUser)
            ? authorizedUser
            : null
        ),
        map((authorizedUser: AuthorizedUser | null) => {
          // Store the authorized user for access throughout registration, which
          // will allows be the most up-to-date authorized user
          this.authorizedUserService.authorizedUser = authorizedUser;

          // Determine the next route based on the existence and status of
          // the authorized user
          return this.routeDestination(routePath, params, authorizedUser);
        })
      );
  }

  private routeDestination(routePath: string, params: Params, authorizedUser: AuthorizedUser | null) {
    // On login the user will always be redirected to the collection notice
    if (routePath.includes(HealthAuthSiteRegRoutes.COLLECTION_NOTICE)) {
      return true;
    }

    switch (authorizedUser?.status) {
      case AccessStatusEnum.UNDER_REVIEW: {
        return this.manageUnderReviewAuthorizedUser(routePath);
      }
      case AccessStatusEnum.APPROVED: {
        return this.manageApprovedAuthorizedUser(routePath);
      }
      case AccessStatusEnum.ACTIVE: {
        return this.manageActiveAuthorizedUser(routePath);
      }
      case AccessStatusEnum.DECLINED: {
        return this.manageDeclinedAuthorizedUser(routePath);
      }
    }

    // Otherwise, no authorized user exists
    return this.manageNoAuthorizedUser(routePath);
  }

  private manageUnderReviewAuthorizedUser(routePath: string): boolean {
    return this.navigate(routePath, [
      HealthAuthSiteRegRoutes.ACCESS,
      HealthAuthSiteRegRoutes.ACCESS_REQUESTED
    ]);
  }

  private manageApprovedAuthorizedUser(routePath: string): boolean {
    return this.navigate(routePath, [
      HealthAuthSiteRegRoutes.ACCESS,
      HealthAuthSiteRegRoutes.ACCESS_APPROVED
    ]);
  }

  private manageDeclinedAuthorizedUser(routePath: string): boolean {
    return this.navigate(routePath, [
      HealthAuthSiteRegRoutes.ACCESS,
      HealthAuthSiteRegRoutes.ACCESS_DECLINED
    ]);
  }

  private manageActiveAuthorizedUser(routePath: string): boolean {
    // Remove query params as they aren't needed to determine
    // the viability of the destination
    const destinationSegment = routePath
      .split('?').shift()
      .split('/').pop();

    const allowedRoutes = HealthAuthSiteRegRoutes.siteRegistrationRouteOrder();

    // Redirect to an appropriate default route
    if (!allowedRoutes.includes(destinationSegment)) {
      return this.navigate(routePath, [
        HealthAuthSiteRegRoutes.SITE_MANAGEMENT
      ]);
    }

    // Otherwise, allow access to the route
    return true;
  }

  private manageNoAuthorizedUser(routePath: string): boolean {
    return this.navigate(routePath, [
      HealthAuthSiteRegRoutes.ACCESS,
      HealthAuthSiteRegRoutes.ACCESS_AUTHORIZED_USER
    ]);
  }

  /**
   * @description
   * Prevent infinite route loops by navigating to a route only
   * when the current route path is not the destination path.
   */
  private navigate(routePath: string, destinationSegments: string[]): boolean {
    const destinationPath = HealthAuthSiteRegRoutes.routePath(destinationSegments.join('/'));

    // Route path may contain query parameters, which should be ignored
    // during the check, but allowed to persist
    if (routePath.split('?').shift() === destinationPath) {
      return true;
    } else {
      this.router.navigate([destinationPath]);
      return false;
    }
  }
}
