import { Component, OnInit } from '@angular/core';

import { Observable, of } from 'rxjs';

import { DashboardMenuItem, DashboardRouteMenuItem } from '@lib/modules/dashboard/models/dashboard-menu-item.model';
import { IDashboard } from '@lib/modules/dashboard/interfaces/dashboard.interface';

import { AuthRoutes } from '@auth/auth.routes';
import { AdjudicationRoutes } from '@adjudication/adjudication.routes';

@Component({
  selector: 'app-adjudication-dashboard',
  templateUrl: './adjudication-dashboard.component.html',
  styleUrls: ['./adjudication-dashboard.component.scss']
})
export class AdjudicationDashboardComponent implements OnInit, IDashboard {
  public dashboardMenuItems: Observable<DashboardMenuItem[]>;
  public logoutRedirectUrl: string;

  constructor() {
    this.logoutRedirectUrl = AuthRoutes.routePath(AuthRoutes.ADMIN);
  }

  public ngOnInit(): void {
    this.dashboardMenuItems = this.getDashboardMenuItems();
  }

  private getDashboardMenuItems(): Observable<DashboardMenuItem[]> {
    return of([
      new DashboardRouteMenuItem('PRIME Enrollees', AdjudicationRoutes.ENROLLEES, 'people'),
      new DashboardRouteMenuItem('Site Registrations', AdjudicationRoutes.SITE_REGISTRATIONS, 'store')
    ]);
  }
}
