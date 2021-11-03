import { Inject, Injectable } from '@angular/core';
import { Params, Router } from '@angular/router';

import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { SiteStatusType } from '@lib/enums/site-status.enum';
import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { BaseGuard } from '@core/guards/base.guard';
import { ConsoleLoggerService } from '@core/services/console-logger.service';
import { HealthAuthorityResource } from '@core/resources/health-authority-resource.service';
import { ToastService } from '@core/services/toast.service';
import { AuthService } from '@auth/shared/services/auth.service';

import { HealthAuthSiteRegRoutes } from '@health-auth/health-auth-site-reg.routes';
import { HealthAuthoritySite } from '@health-auth/shared/models/health-authority-site.model';
import { HealthAuthoritySiteService } from '@health-auth/shared/services/health-authority-site.service';

@Injectable({
  providedIn: 'root'
})
export class HealthAuthoritySiteGuard extends BaseGuard {
  constructor(
    protected authService: AuthService,
    protected logger: ConsoleLoggerService,
    @Inject(APP_CONFIG) private config: AppConfig,
    private router: Router,
    private healthAuthoritySiteService: HealthAuthoritySiteService,
    private healthAuthorityResource: HealthAuthorityResource,
    private toastService: ToastService
  ) {
    super(authService, logger);
  }

  protected checkAccess(routePath: string, params?: Params): Observable<boolean> | Promise<boolean> {
    return this.healthAuthorityResource.getHealthAuthoritySiteById(params.haid, params.sid)
      .pipe(
        map((healthAuthoritySite: HealthAuthoritySite) =>
          // Store the site for access throughout registration, which
          // will allows be the most up-to-date site information
          this.healthAuthoritySiteService.site = healthAuthoritySite
        ),
        map((healthAuthoritySite: HealthAuthoritySite) =>
          this.routeDestination(routePath, params, healthAuthoritySite)
        )
      );
  }

  private routeDestination(routePath: string, params: Params, healthAuthoritySite: HealthAuthoritySite): boolean {
    switch (healthAuthoritySite?.status) {
      case SiteStatusType.EDITABLE: {
        return this.manageEditableHealthAuthoritySite(routePath, params);
      }
      case SiteStatusType.IN_REVIEW: {
        return this.manageInReviewHealthAuthoritySite(routePath, params);
      }
      case SiteStatusType.LOCKED: {
        return this.manageLockedHealthAuthoritySite(routePath, params);
      }
    }

    // Otherwise, no health authority site exists
    return this.manageNoHealthAuthoritySite(routePath, params);
  }

  /**
   * @description
   * Restrict access to routes permitted when "Editable".
   */
  // TODO what is editable in health authority site registration?
  // TODO editable and !approved vs editable and approved
  private manageEditableHealthAuthoritySite(routePath: string, params: Params): boolean {
    return true;
  }

  /**
   * @description
   * Restrict access to routes permitted when "In Review".
   */
  private manageInReviewHealthAuthoritySite(routePath: string, params: Params): boolean {
    return this.navigate(routePath, [
      HealthAuthSiteRegRoutes.HEALTH_AUTHORITIES,
      params.haid,
      HealthAuthSiteRegRoutes.SITES,
      params.sid,
      HealthAuthSiteRegRoutes.SITE_OVERVIEW
    ]);
  }

  /**
   * @description
   * Restrict access to routes permitted when "Locked".
   */
  // TODO what does locking a site mean in health authority site registration?
  private manageLockedHealthAuthoritySite(routePath: string, params: Params): boolean {
    return this.navigate(routePath, [
      HealthAuthSiteRegRoutes.SITE_MANAGEMENT
    ]);
  }

  /**
   * @description
   * Restrict access to routes permitted when the site
   * does not exist.
   */
  private manageNoHealthAuthoritySite(routePath: string, params: Params): boolean {
    const newSiteRoutePath = [
      HealthAuthSiteRegRoutes.HEALTH_AUTHORITIES,
      params.haid,
      HealthAuthSiteRegRoutes.SITES,
      0,
      HealthAuthSiteRegRoutes.VENDOR
    ].join('/');

    if (routePath.includes(newSiteRoutePath)) {
      return true;
    }

    this.toastService.openErrorToast('Site being accessed does not exist');

    return this.navigate(routePath, [
      HealthAuthSiteRegRoutes.SITE_MANAGEMENT
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
