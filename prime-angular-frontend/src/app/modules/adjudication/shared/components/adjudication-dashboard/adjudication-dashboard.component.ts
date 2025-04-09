import { Component, Inject, OnInit } from '@angular/core';

import { Observable, of } from 'rxjs';

import { DashboardMenuItem, DashboardRouteMenuItem } from '@lib/modules/dashboard/models/dashboard-menu-item.model';
import { IDashboard } from '@lib/modules/dashboard/interfaces/dashboard.interface';

import { AppConfig, APP_CONFIG } from 'app/app-config.module';
import { AdjudicationRoutes } from '@adjudication/adjudication.routes';

@Component({
  selector: 'app-adjudication-dashboard',
  templateUrl: './adjudication-dashboard.component.html',
  styleUrls: ['./adjudication-dashboard.component.scss']
})
export class AdjudicationDashboardComponent implements OnInit, IDashboard {
  public dashboardMenuItems: Observable<DashboardMenuItem[]>;
  public logoutRedirectUrl: string;

  constructor(
    @Inject(APP_CONFIG) protected config: AppConfig
  ) {
    this.logoutRedirectUrl = `${this.config.loginRedirectUrl}/${AdjudicationRoutes.LOGIN_PAGE}`;
  }

  public ngOnInit(): void {
    this.dashboardMenuItems = this.getDashboardMenuItems();
  }

  private getDashboardMenuItems(): Observable<DashboardMenuItem[]> {
    return of([
      new DashboardRouteMenuItem('PRIME Enrollees', AdjudicationRoutes.ENROLLEES, 'people'),
      new DashboardRouteMenuItem('Organizations', AdjudicationRoutes.ORGANIZATIONS, 'location_city'),
      new DashboardRouteMenuItem('Site Registrations', AdjudicationRoutes.SITE_REGISTRATIONS, 'store'),
      new DashboardRouteMenuItem('Admin Users', AdjudicationRoutes.ADMIN_USERS, 'admin_panel_settings'),
      new DashboardRouteMenuItem('Metabase Reports', AdjudicationRoutes.METABASE_REPORTS, 'show_chart')
    ]);
  }
}
