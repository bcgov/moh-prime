import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { AdjudicationRoutes } from '@adjudication/adjudication.routes';

@Component({
  selector: 'app-site-maintenance-page',
  templateUrl: './site-maintenance-page.component.html',
  styleUrls: ['./site-maintenance-page.component.scss']
})
export class SiteMaintenancePageComponent implements OnInit {
  private routeUtils: RouteUtils;

  constructor(
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.routeUtils = new RouteUtils(route, router, AdjudicationRoutes.routePath(AdjudicationRoutes.MAINTENANCE));
  }

  public onBack() {
    this.routeUtils.routeRelativeTo(['../']);
  }

  public ngOnInit(): void { }
}
