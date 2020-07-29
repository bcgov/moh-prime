import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { AuthService } from '@auth/shared/services/auth.service';
import { SiteRoutes } from '@registration/site-registration.routes';
import { RouteUtils } from '@registration/shared/classes/route-utils.class';
import { OrganizationService } from '@registration/shared/services/organization.service';

@Component({
  selector: 'app-collection-notice',
  templateUrl: './collection-notice.component.html',
  styleUrls: ['./collection-notice.component.scss']
})
export class CollectionNoticeComponent implements OnInit {
  public isFull: boolean;
  public routeUtils: RouteUtils;
  public isCompleted: boolean;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthService,
    private organizationService: OrganizationService
  ) {
    this.isFull = true;
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.MODULE_PATH);
  }

  public onAccept() {
    this.authService.hasJustLoggedIn = false;
    this.routeUtils.routeRelativeTo(SiteRoutes.SITE_MANAGEMENT);
  }

  public ngOnInit() {
    const organization = this.organizationService.organization;
    this.authService.hasJustLoggedIn = true;

    // Collection notice is the initial route after login, and used as a hub
    // for redirection to an appropriate view based on the organization
    organization.completed
      ? this.router.navigate([SiteRoutes.SITE_MANAGEMENT], { relativeTo: this.route.parent })
      : this.router.navigate(
        [SiteRoutes.SITE_MANAGEMENT, organization.id, SiteRoutes.ORGANIZATION_SIGNING_AUTHORITY],
        { relativeTo: this.route.parent }
      );
  }
}
