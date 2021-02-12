import { Injectable, Inject } from '@angular/core';
import { Router } from '@angular/router';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { BaseGuard } from '@core/guards/base.guard';
import { LoggerService } from '@core/services/logger.service';
import { AuthService } from '@auth/shared/services/auth.service';
import { AuthRoutes } from '@auth/auth.routes';

@Injectable({
  providedIn: 'root'
})
export class AuthorizationRedirectGuard extends BaseGuard {
  constructor(
    protected authService: AuthService,
    protected logger: LoggerService,
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

      if (this.authService.isPhsa()) {
        destinationRoute = this.config.routes.phsa;
      } else if (this.authService.isEnrollee()) {
        destinationRoute = (routePath.slice(1) === AuthRoutes.SITE)
          ? this.config.routes.site
          : this.config.routes.enrolment;
      } else if (this.authService.hasAdminView()) {
        destinationRoute = this.config.routes.adjudication;
      }

      // Otherwise, redirect to an appropriate destination
      this.router.navigate([destinationRoute]);
      return reject(false);
    });
  }
}
