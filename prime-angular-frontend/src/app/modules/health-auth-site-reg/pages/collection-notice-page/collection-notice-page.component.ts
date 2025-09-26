import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { AuthService } from '@auth/shared/services/auth.service';

import { HealthAuthSiteRegRoutes } from '@health-auth/health-auth-site-reg.routes';

@Component({
  selector: 'app-collection-notice-page',
  templateUrl: './collection-notice-page.component.html',
  styleUrls: ['./collection-notice-page.component.scss']
})
export class CollectionNoticePageComponent implements OnInit {
  public isFull: boolean;
  public routeUtils: RouteUtils;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthService
  ) {
    this.isFull = true;
    this.routeUtils = new RouteUtils(route, router, HealthAuthSiteRegRoutes.MODULE_PATH);
  }

  public ngOnInit(): void {
    this.authService.hasJustLoggedIn = true;
    this.authService.passcode = this.route.snapshot.queryParams['pc'] ? atob(this.route.snapshot.queryParams['pc']) : null;

    console.log(this.authService.passcode);

    this.nextRoute();
  }

  private nextRoute(): void {
    // Display of the Collection Notice has moved to SiteRegAccessComponent
    // so we jump through this component but leave it in place for other routing logic

    this.authService.hasJustLoggedIn = false;

    // Redirect to site management and allow the guards to
    // figure out the proper routing
    this.routeUtils.routeRelativeTo(HealthAuthSiteRegRoutes.SITE_MANAGEMENT);
  }
}
