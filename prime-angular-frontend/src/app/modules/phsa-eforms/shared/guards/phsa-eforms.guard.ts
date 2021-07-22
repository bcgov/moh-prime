import { Inject } from '@angular/core';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

import { Observable, of } from 'rxjs';

import { AppConfig, APP_CONFIG } from 'app/app-config.module';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { BaseGuard } from '@core/guards/base.guard';
import { ConsoleLoggerService } from '@core/services/console-logger.service';
import { AuthService } from '@auth/shared/services/auth.service';

import { PhsaEformsRoutes } from '@phsa/phsa-eforms.routes';

@Injectable({
  providedIn: 'root'
})
export class PhsaEformsGuard extends BaseGuard {

  public constructor(
    protected authService: AuthService,
    protected logger: ConsoleLoggerService,
    @Inject(APP_CONFIG) private config: AppConfig,
    private router: Router
  ) {
    super(authService, logger);
  }

  protected checkAccess(routePath: string = null): Observable<boolean> | Promise<boolean> {
    // On login the user will always be redirected to the collection notice
    if (routePath.includes(PhsaEformsRoutes.COLLECTION_NOTICE)) {
      return of(true);
    }

    // Current route path is empty when URL manually inserted by user
    const currentRoutePath = RouteUtils.currentRoutePath(this.router.url);
    const nextRoutePath = RouteUtils.currentRoutePath(routePath);

    if (
      currentRoutePath === PhsaEformsRoutes.DEMOGRAPHIC &&
      nextRoutePath === PhsaEformsRoutes.AVAILABLE_ACCESS
    ) {
      return of(this.navigate(routePath, PhsaEformsRoutes.AVAILABLE_ACCESS));
    } else if (
      currentRoutePath === PhsaEformsRoutes.AVAILABLE_ACCESS &&
      nextRoutePath === PhsaEformsRoutes.SUBMISSION_CONFIRMATION
    ) {
      return of(this.navigate(routePath, PhsaEformsRoutes.SUBMISSION_CONFIRMATION));
    } else {
      // Otherwise, start at the beginning of the enrolment process
      return of(this.navigate(routePath, PhsaEformsRoutes.DEMOGRAPHIC));
    }
  }

  /**
   * @description
   * Similar to code from EnrolmentGuard.navigate
   */
  private navigate(routePath: string, destinationPath: string): boolean {
    const phsaRoutePath = this.config.routes.phsa;

    if (routePath === `/${phsaRoutePath}/${destinationPath}`) {
      return true;
    } else {
      this.router.navigate([phsaRoutePath, destinationPath]);
      return false;
    }
  }
}
