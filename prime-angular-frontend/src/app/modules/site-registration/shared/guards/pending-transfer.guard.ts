import { Inject, Injectable } from '@angular/core';
import { CanActivate, CanActivateChild, Router } from '@angular/router';

import { AppConfig, APP_CONFIG } from 'app/app-config.module';
import { SiteRoutes } from '@registration/site-registration.routes';
import { OrganizationService } from '@registration/shared/services/organization.service';

@Injectable({
  providedIn: 'root'
})
export class PendingTransferGuard implements CanActivate, CanActivateChild {
  constructor(
    private organizationService: OrganizationService,
    private router: Router,
    @Inject(APP_CONFIG) private config: AppConfig,
  ) { }

  public canActivate(): boolean | Promise<boolean> {
    return this.routeDestination();
  }

  public canActivateChild(): boolean | Promise<boolean> {
    return this.routeDestination();
  }

  /**
   * @description
   * Determine the route destination based on the site status.
   */
  private routeDestination(): boolean | Promise<boolean> {
    if (this.organizationService.organization.pendingTransfer) {
      const modulePath = this.config.routes.site;
      return this.router.navigate([modulePath, SiteRoutes.SITE_MANAGEMENT]);
    }
    return true;
  }
}
