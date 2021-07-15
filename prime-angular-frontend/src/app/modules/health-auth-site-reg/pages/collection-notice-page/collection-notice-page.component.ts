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

  public onAccept(): void {
    this.authService.hasJustLoggedIn = false;
    this.nextRoute();
  }

  public ngOnInit(): void {
    this.authService.hasJustLoggedIn = true;
  }

  private nextRoute(): void {
    // Redirect to site management and allow the guards to
    // figure out the proper routing
    this.routeUtils.routeRelativeTo(HealthAuthSiteRegRoutes.SITE_MANAGEMENT);
  }
}
