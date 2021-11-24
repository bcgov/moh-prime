import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { SiteRoutes } from '@registration/site-registration.routes';

@Component({
  selector: 'app-organization-claimed-page',
  templateUrl: './organization-claimed-page.component.html',
  styleUrls: ['./organization-claimed-page.component.scss']
})
export class OrganizationClaimedPageComponent implements OnInit {
  public title: string;
  public isCompleted: boolean;

  private readonly routeUtils: RouteUtils;

  constructor(
    private route: ActivatedRoute,
    router: Router
  ) {
    this.title = this.route.snapshot.data.title;

    this.routeUtils = new RouteUtils(route, router, SiteRoutes.MODULE_PATH);
  }

  public onRoute(event: Event): void {
    event.preventDefault();
    this.routeUtils.routeWithin([SiteRoutes.ORGANIZATIONS]);
  }

  public ngOnInit(): void {
    this.isCompleted = true;
  }
}
