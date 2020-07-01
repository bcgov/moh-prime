import { Injectable, Inject } from '@angular/core';
import { Router } from '@angular/router';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { BaseGuard } from '@core/guards/base.guard';
import { LoggerService } from '@core/services/logger.service';
import { AuthenticationService } from '@auth/shared/services/authentication.service';

@Injectable({
  providedIn: 'root'
})
export class RegistrantGuard extends BaseGuard {
  constructor(
    protected authenticationService: AuthenticationService,
    protected logger: LoggerService,
    @Inject(APP_CONFIG) private config: AppConfig,
    private router: Router
  ) {
    super(authenticationService, logger);
  }

  /**
   * @description
   * Check the user is authenticated, otherwise redirect
   * them to an appropriate destination.
   */
  protected canAccess(authenticated: boolean, routePath: string): Promise<boolean> {
    return new Promise(async (resolve, reject) => {
      let destinationRoute = this.config.routes.denied;

      if (!authenticated) {
        destinationRoute = this.config.routes.auth;
      } else if (this.authenticationService.isRegistrant()) {
        // Allow route to resolve
        return resolve(true);
      } else if (this.authenticationService.hasEnrollee()) {
        destinationRoute = this.config.routes.enrolment;
      }

      // Otherwise, redirect to an appropriate destination
      this.router.navigate([destinationRoute]);
      return reject(false);
    });
  }
}
