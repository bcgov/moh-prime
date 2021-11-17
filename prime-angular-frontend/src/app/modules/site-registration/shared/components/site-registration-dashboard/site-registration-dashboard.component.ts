import { Component, Inject, OnInit } from '@angular/core';

import { Observable, of } from 'rxjs';

import { DashboardMenuItem, DashboardRouteMenuItem } from '@lib/modules/dashboard/models/dashboard-menu-item.model';
import { IDashboard } from '@lib/modules/dashboard/interfaces/dashboard.interface';

import { AppConfig, APP_CONFIG } from 'app/app-config.module';
import { SiteRoutes } from '@registration/site-registration.routes';

@Component({
  selector: 'app-site-registration-dashboard',
  templateUrl: './site-registration-dashboard.component.html',
  styleUrls: ['./site-registration-dashboard.component.scss']
})
export class SiteRegistrationDashboardComponent implements OnInit, IDashboard {
  public dashboardMenuItems: Observable<DashboardMenuItem[]>;
  public logoutRedirectUrl: string;

  constructor(
    @Inject(APP_CONFIG) protected config: AppConfig
  ) {
    this.logoutRedirectUrl = `${ this.config.loginRedirectUrl }/${ SiteRoutes.LOGIN_PAGE }`;
  }

  public ngOnInit(): void {
    this.dashboardMenuItems = this.getDashboardMenuItems();
  }

  private getDashboardMenuItems(): Observable<DashboardMenuItem[]> {
    return of([
      new DashboardRouteMenuItem('Site Management', SiteRoutes.ORGANIZATIONS, 'store', true)
    ]);
  }
}
