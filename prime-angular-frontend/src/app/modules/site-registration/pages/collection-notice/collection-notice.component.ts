import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { AuthService } from '@auth/shared/services/auth.service';
import { SiteRoutes } from '@registration/site-registration.routes';
import { SiteRegistrationService } from '@registration/shared/services/site-registration.service';
import { RouteUtils } from '@registration/shared/classes/route-utils.class';

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
    private siteRegistrationService: SiteRegistrationService
  ) {
    this.isFull = true;
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.MODULE_PATH);
  }

  public onAccept() {
    this.authService.hasJustLoggedIn = false;
    this.routeUtils.routeRelativeTo(SiteRoutes.MULTIPLE_SITES);
  }

  public ngOnInit() {
    this.authService.hasJustLoggedIn = true;
    this.isCompleted = this.siteRegistrationService.site.complete;

    if (this.isCompleted) {
      this.routeUtils.routeRelativeTo(SiteRoutes.SITE_REVIEW);
    }
  }
}
