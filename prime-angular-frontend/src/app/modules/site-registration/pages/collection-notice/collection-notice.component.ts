import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { AuthenticationService } from '@auth/shared/services/authentication.service';
import { SiteRoutes } from '@registration/site-registration.routes';
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
    private authenticationService: AuthenticationService
  ) {
    this.isFull = true;
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.MODULE_PATH);
  }

  public onAccept() {
    this.authenticationService.hasJustLoggedIn = false;
    this.routeUtils.routeRelativeTo(SiteRoutes.ORGANIZATIONS);
  }

  public ngOnInit() {
    this.authenticationService.hasJustLoggedIn = true;
  }
}
