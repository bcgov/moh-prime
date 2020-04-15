import { Injectable, Inject } from '@angular/core';
import { Router } from '@angular/router';

import { BaseGuard } from '@core/guards/base.guard';
import { LoggerService } from '@core/services/logger.service';

import { AppConfig, APP_CONFIG } from 'app/app-config.module';
import { AuthService } from '@auth/shared/services/auth.service';

import { SiteRoutes } from '@registration/site-registration.routes';

@Injectable({
  providedIn: 'root'
})
export class SiteRegistrationGuard extends BaseGuard {

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
   * Check an enrollee enrolment status, and attempt to redirect
   * to an appropriate destination based on its existence or
   * status.
   */
  protected canAccess(authenticated: boolean, routePath: string): Promise<boolean> {
    return new Promise(async (resolve, reject) => {
      const currentBaseRoute = this.router.url.slice(1).split('/')[0];
      const currentRoute = this.router.url.slice(1).split('/')[1];

      if (this.authService.isRegistrant()) {
        return resolve(true);
      } else if (this.authService.isEnrollee()) {
        this.router.navigate([this.config.routes.enrolment]);
      }
      return reject(false);
    });
  }
}
