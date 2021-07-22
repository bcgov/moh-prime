import { Injectable, Inject } from '@angular/core';
import { Router } from '@angular/router';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { BaseGuard } from '@core/guards/base.guard';
import { ConsoleLoggerService } from '@core/services/console-logger.service';
import { AuthService } from '@auth/shared/services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class GisAuthorizationRedirectGuard extends BaseGuard {
  constructor(
    protected authService: AuthService,
    protected logger: ConsoleLoggerService,
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

      // Otherwise, redirect to an appropriate destination
      this.router.navigate([this.config.routes.gis]);
      return reject(false);
    });
  }
}
