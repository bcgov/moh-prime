import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { RouteUtils } from '@lib/utils/route-utils.class';

import { AdjudicationRoutes } from '@adjudication/adjudication.routes';

@Component({
  selector: 'app-enrollee-maintenance',
  templateUrl: './enrollee-maintenance.component.html',
  styleUrls: ['./enrollee-maintenance.component.scss']
})
export class EnrolleeMaintenanceComponent implements OnInit {
  public AdjudicationRoutes = AdjudicationRoutes;
  
  private routeUtils: RouteUtils;

  constructor(
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.routeUtils = new RouteUtils(route, router, AdjudicationRoutes.routePath(AdjudicationRoutes.ENROLLEES));
  }

  public onRoute(routePath: string | (string | number)[]): void {
    this.routeUtils.routeRelativeTo(routePath);
  }

  public ngOnInit(): void { }
}
