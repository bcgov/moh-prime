import { Inject, Injectable } from '@angular/core';
import { Params, Router } from '@angular/router';

import { Observable } from 'rxjs';
import { exhaustMap, map } from 'rxjs/operators';

import { BaseGuard } from '@core/guards/base.guard';
import { LoggerService } from '@core/services/logger.service';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { BcscUser } from '@auth/shared/models/bcsc-user.model';
import { AuthService } from '@auth/shared/services/auth.service';
import { AuthorizedUser } from '@health-auth/shared/models/authorized-user.model';
import { HealthAuthSiteRegService } from '@health-auth/shared/services/health-auth-site-reg.service';
import { HealthAuthSiteRegResource } from '@health-auth/shared/resources/health-auth-site-reg-resource.service';
import { SiteRoutes } from '@registration/site-registration.routes';
import { AuthorizedUserStatusEnum } from '@health-auth/shared/enums/authorized-user-status.enum';

@Injectable({
  providedIn: 'root'
})
export class AuthorizedUserGuard extends BaseGuard {
  constructor(
    protected authService: AuthService,
    protected logger: LoggerService,
    @Inject(APP_CONFIG) private config: AppConfig,
    private router: Router,
    private healthAuthSiteRegService: HealthAuthSiteRegService,
    private healthAuthSiteRegResource: HealthAuthSiteRegResource
  ) {
    super(authService, logger);
  }

  protected checkAccess(routePath: string = null, params?: Params): Observable<boolean> | Promise<boolean> {
    return this.authService.getUser$()
      .pipe(
        // Having no authorized user in the same redirection logic for the user as
        // an unapproved status, and therefore not handled individually
        exhaustMap((user: BcscUser) =>
          this.healthAuthSiteRegResource.getAuthorizedUserByUserId(user.userId)
        ),
        map((authorizedUser: AuthorizedUser | null) =>
          (authorizedUser)
            ? authorizedUser
            : null
        ),
        map((authorizedUser: AuthorizedUser | null) => {
          // Determine the next route based on the existence and status of
          // the authorized user
          return this.routeDestination(routePath, params, authorizedUser);
        })
      );
  }

  private routeDestination(routePath: string, params: Params, authorizedUser: AuthorizedUser | null) {
    // TODO add this into a base enrolment guard
    // On login the user will always be redirected to the collection notice
    if (routePath.includes(SiteRoutes.COLLECTION_NOTICE)) {
      return true;
    }

    switch (authorizedUser?.status) {
      case AuthorizedUserStatusEnum.APPROVED: {
        return this.manageApprovedAuthorizedUser(routePath);
      }
      case AuthorizedUserStatusEnum.DECLINED: {
        return this.manageDeclinedAuthorizedUser(routePath);
      }
      case AuthorizedUserStatusEnum.UNDER_REVIEW: {
        return this.manageUnderReviewAuthorizedUser(routePath);
      }
    }

    // Otherwise, no authorized user exists
    return this.manageNoAuthorizedUser(routePath);
  }

  private manageApprovedAuthorizedUser(routePath: string): boolean {
    return true;
  }

  private manageDeclinedAuthorizedUser(routePath: string): boolean {
    return true;
  }

  private manageUnderReviewAuthorizedUser(routePath: string): boolean {
    return true;
  }

  private manageNoAuthorizedUser(routePath: string): boolean {
    return true;
  }

  /**
   * @description
   * Prevent infinite route loops by navigating to a route only
   * when the current route path is not the destination path.
   */
  // private navigate(
  //   routePath: string,
  //   loopPath: string,
  //   destinationPath: string = null,
  //   oid: number = null
  // ): boolean {
  //   const modulePath = this.config.routes.site;
  //   const comparePath = (destinationPath && oid !== null)
  //     ? `/${modulePath}/${loopPath}/${oid}/${destinationPath}`
  //     : `/${modulePath}/${loopPath}`;
  //
  //   if (routePath === comparePath) {
  //     return true;
  //   } else {
  //     this.router.navigate([comparePath]);
  //     return false;
  //   }
  // }
}
