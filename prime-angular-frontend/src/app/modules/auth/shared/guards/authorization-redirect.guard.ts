import { Injectable, Inject } from '@angular/core';
import { Router } from '@angular/router';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { BaseGuard } from '@core/guards/base.guard';
import { ConsoleLoggerService } from '@core/services/console-logger.service';
import { AuthService } from '@auth/shared/services/auth.service';
import { PermissionService } from '@auth/shared/services/permission.service';
import { Role } from '../enum/role.enum';
import { SiteRoutes } from '@registration/site-registration.routes';

@Injectable({
  providedIn: 'root'
})
export class AuthorizationRedirectGuard extends BaseGuard {
  constructor(
    protected authService: AuthService,
    protected logger: ConsoleLoggerService,
    private permissionService: PermissionService,
    @Inject(APP_CONFIG) private config: AppConfig,
    private router: Router
  ) {
    super(authService, logger);
  }

  /**
   * @description
   * Attempt to redirect an authenticated user to an appropriate
   * destination when possible, otherwise prompt user to
   * authenticate.
   */
  protected canAccess(authenticated: boolean, routePath: string): Promise<boolean> {
    return new Promise((resolve, reject) => {
      if (!authenticated) {
        // Allow route to resolve for user to authenticate
        return resolve(true);
      }

      let destinationRoute = this.config.routes.denied;

      if (this.permissionService.hasAnyRole([Role.PHSA_LABTECH, Role.PHSA_IMMUNIZER])) {
        destinationRoute = this.config.routes.phsa;
      } else if (this.permissionService.hasRoles(Role.ENROLLEE)) {
        destinationRoute = (routePath.slice(1) === SiteRoutes.LOGIN_PAGE)
          ? this.config.routes.site
          : this.config.routes.enrolment;
      } else if (this.permissionService.hasRoles(Role.ADMIN)) {
        destinationRoute = this.config.routes.adjudication;
      }

      // Otherwise, redirect to an appropriate destination
      this.router.navigate([destinationRoute]);
      return reject(false);
    });
  }
}
