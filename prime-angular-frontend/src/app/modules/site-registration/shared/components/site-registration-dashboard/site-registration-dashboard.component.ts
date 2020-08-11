import { Component, OnInit } from '@angular/core';

import { Observable, of } from 'rxjs';

import { DashboardMenuItem, DashboardRouteMenuItem } from '@lib/modules/dashboard/models/dashboard-menu-item.model';
import { IDashboard } from '@lib/modules/dashboard/interfaces/dashboard.interface';

import { AuthRoutes } from '@auth/auth.routes';
import { SiteRoutes } from '@registration/site-registration.routes';

@Component({
  selector: 'app-site-registration-dashboard',
  templateUrl: './site-registration-dashboard.component.html',
  styleUrls: ['./site-registration-dashboard.component.scss']
})
export class SiteRegistrationDashboardComponent implements OnInit, IDashboard {
  public dashboardMenuItems: Observable<DashboardMenuItem[]>;
  public logoutRedirectUrl: string;

  constructor() {
    this.logoutRedirectUrl = AuthRoutes.routePath(AuthRoutes.SITE);
  }

  public ngOnInit(): void {
    this.dashboardMenuItems = this.getDashboardMenuItems();
  }

  private getDashboardMenuItems(): Observable<DashboardMenuItem[]> {
    return of([
      new DashboardRouteMenuItem('Site Management', SiteRoutes.SITE_MANAGEMENT, 'store', true)
    ]);
  }
}
