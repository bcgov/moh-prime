import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { AdjudicationRoutes } from '@adjudication/adjudication.routes';

@Component({
  selector: 'app-enrollee-maintenance-page',
  templateUrl: './enrollee-maintenance-page.component.html',
  styleUrls: ['./enrollee-maintenance-page.component.scss']
})
export class EnrolleeMaintenancePageComponent implements OnInit {
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
