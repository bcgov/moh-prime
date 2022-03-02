import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { RoutePath, RouteUtils } from '@lib/utils/route-utils.class';

import { AuthService } from '@auth/shared/services/auth.service';

import { SiteRoutes } from '@registration/site-registration.routes';
import { OrganizationService } from '@registration/shared/services/organization.service';

@Component({
  selector: 'app-collection-notice-page',
  templateUrl: './collection-notice-page.component.html',
  styleUrls: ['./collection-notice-page.component.scss']
})
export class CollectionNoticePageComponent implements OnInit {
  public isFull: boolean;

  private readonly routeUtils: RouteUtils;
  private readonly nextRoute: RoutePath;

  constructor(
    private route: ActivatedRoute,
    private authService: AuthService,
    private organizationService: OrganizationService,
    router: Router
  ) {
    this.isFull = true;
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.MODULE_PATH);
    this.nextRoute = this.route.snapshot.data.redirectRouteSegments.nextRoute;
  }

  public ngOnInit(): void {
    // Display of the Collection Notice has moved to SiteRegAccessComponent
    // so we jump through this component but leave it in place for other routing logic

    this.authService.hasJustLoggedIn = false;
    // Attempt to redirect to centralized default route, and the guard will
    // redirect an appropriate route when not allowed
    this.routeUtils.routeRelativeTo(this.nextRoute);
  }
}
