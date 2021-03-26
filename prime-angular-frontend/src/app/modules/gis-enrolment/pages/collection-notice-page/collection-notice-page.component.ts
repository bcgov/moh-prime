import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { AuthService } from '@auth/shared/services/auth.service';

import { GisEnrolmentRoutes } from '@gis/gis-enrolment.routes';

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
    this.routeUtils = new RouteUtils(route, router, GisEnrolmentRoutes.MODULE_PATH);
  }

  public onAccept() {
    this.authService.hasJustLoggedIn = false;

    this.router.navigate([GisEnrolmentRoutes.LDAP_USER_PAGE], { relativeTo: this.route.parent });
  }

  public ngOnInit(): void {
    // TODO temporarily commented out until a GIS collection notice is provided
    // this.authService.hasJustLoggedIn = true;
    // TODO added until a GIS collection notice is provided
    this.router.navigate([GisEnrolmentRoutes.LDAP_USER_PAGE], { relativeTo: this.route.parent });
  }
}
