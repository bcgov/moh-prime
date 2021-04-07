import { Component, Inject, OnInit } from '@angular/core';

import { Observable, of } from 'rxjs';

import { DashboardMenuItem, DashboardRouteMenuItem } from '@lib/modules/dashboard/models/dashboard-menu-item.model';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { HealthAuthSiteRegRoutes } from '@health-auth/health-auth-site-reg.routes';

@Component({
  selector: 'app-health-auth-site-reg-dashboard',
  templateUrl: './health-auth-site-reg-dashboard.component.html',
  styleUrls: ['./health-auth-site-reg-dashboard.component.scss']
})
export class HealthAuthSiteRegDashboardComponent implements OnInit {
  public dashboardMenuItems: Observable<DashboardMenuItem[]>;
  public logoutRedirectUrl: string;

  constructor(
    @Inject(APP_CONFIG) protected config: AppConfig
  ) {
    this.logoutRedirectUrl = `${ this.config.loginRedirectUrl }/${ HealthAuthSiteRegRoutes.LOGIN_PAGE }`;
  }

  public ngOnInit(): void {
    this.dashboardMenuItems = this.getDashboardMenuItems();
  }

  private getDashboardMenuItems(): Observable<DashboardMenuItem[]> {
    return of([
      new DashboardRouteMenuItem('Site Management', HealthAuthSiteRegRoutes.SITE_MANAGEMENT, 'store', true)
    ]);
  }
}
