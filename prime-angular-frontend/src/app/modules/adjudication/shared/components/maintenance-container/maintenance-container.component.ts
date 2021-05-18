import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { RouteUtils } from '@lib/utils/route-utils.class';

@Component({
  selector: 'app-maintenance-container',
  templateUrl: './maintenance-container.component.html',
  styleUrls: ['./maintenance-container.component.scss']
})
export class MaintenanceContainerComponent implements OnInit {

  public AdjudicationRoutes = AdjudicationRoutes;

  private routeUtils: RouteUtils;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
  ) {
    this.routeUtils = new RouteUtils(route, router, AdjudicationRoutes.routePath(AdjudicationRoutes.LOGIN_PAGE));
  }

  public onRoute(routePath: string | (string | number)[]) {
    this.routeUtils.routeRelativeTo(routePath);
  }

  public getEmailSummary() {
    return [
      {
        key: 'View the notification emails',
        value: ''
      }
    ];
  }

  ngOnInit(): void {
  }

}
