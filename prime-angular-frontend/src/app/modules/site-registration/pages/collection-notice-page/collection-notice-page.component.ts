import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { RouteUtils } from '@lib/utils/route-utils.class';

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
  public routeUtils: RouteUtils;

  constructor(
    private route: ActivatedRoute,
    private authService: AuthService,
    private organizationService: OrganizationService,
    router: Router
  ) {
    this.isFull = true;
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.MODULE_PATH);
  }

  public onAccept(): void {
    this.authService.hasJustLoggedIn = false;
    // Attempt to redirect to centralized default route, and the guard will
    // redirect an appropriate route when not allowed
    this.routeUtils.routeRelativeTo([SiteRoutes.ORGANIZATIONS]);
  }

  public ngOnInit(): void {
    this.authService.hasJustLoggedIn = true;
  }
}
