import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { RouteUtils } from '@lib/utils/route-utils.class';

import { AdjudicationRoutes } from '@adjudication/adjudication.routes';


@Component({
  selector: 'app-enrollee-toa-maintenance',
  templateUrl: './enrollee-toa-maintenance.component.html',
  styleUrls: ['./enrollee-toa-maintenance.component.scss']
})
export class EnrolleeToaMaintenanceComponent implements OnInit {
  private routeUtils: RouteUtils;

  public AdjudicationRoutes = AdjudicationRoutes;

  constructor(private route: ActivatedRoute,
    private router: Router
  ) {
    this.routeUtils = new RouteUtils(route, router, AdjudicationRoutes.routePath(AdjudicationRoutes.ENROLLEES));
  }

  public onRoute(routePath: string | (string | number)[]): void {
    this.routeUtils.routeRelativeTo(routePath);
  }

  public ngOnInit(): void {
  }

}
