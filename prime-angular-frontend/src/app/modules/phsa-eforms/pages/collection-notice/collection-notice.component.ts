import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { AuthService } from '@auth/shared/services/auth.service';

import { PhsaEformsRoutes } from '@phsa/phsa-eforms.routes';

@Component({
  selector: 'app-collection-notice',
  templateUrl: './collection-notice.component.html',
  styleUrls: ['./collection-notice.component.scss']
})
export class CollectionNoticeComponent implements OnInit {
  public isFull: boolean;
  public routeUtils: RouteUtils;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthService
  ) {
    this.isFull = true;
    this.routeUtils = new RouteUtils(route, router, PhsaEformsRoutes.MODULE_PATH);
  }

  public onAccept() {
    this.authService.hasJustLoggedIn = false;
    this.nextRoute();
  }

  public ngOnInit(): void {
    // TODO temporarily commented out until a PHSA collection notice is provided
    // this.authService.hasJustLoggedIn = true;
    // TODO added until a collection notice is provided
    this.nextRoute();
  }

  private nextRoute() {
    this.router.navigate([PhsaEformsRoutes.DEMOGRAPHIC], { relativeTo: this.route.parent });
  }
}
