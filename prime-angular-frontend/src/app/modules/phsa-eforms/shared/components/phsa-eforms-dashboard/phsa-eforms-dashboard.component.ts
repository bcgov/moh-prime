import { Component, Inject, OnInit } from '@angular/core';

import { Observable, of } from 'rxjs';

import { DashboardMenuItem, DashboardRouteMenuItem } from '@lib/modules/dashboard/models/dashboard-menu-item.model';
import { IDashboard } from '@lib/modules/dashboard/interfaces/dashboard.interface';

import { AppConfig, APP_CONFIG } from 'app/app-config.module';
import { AuthRoutes } from '@auth/auth.routes';
import { PhsaEformsRoutes } from '@phsa/phsa-eforms.routes';

@Component({
  selector: 'app-phsa-eforms-dashboard',
  templateUrl: './phsa-eforms-dashboard.component.html',
  styleUrls: ['./phsa-eforms-dashboard.component.scss']
})
export class PhsaEformsDashboardComponent implements OnInit, IDashboard {
  public dashboardMenuItems: Observable<DashboardMenuItem[]>;
  public logoutRedirectUrl: string;

  constructor(
    @Inject(APP_CONFIG) protected config: AppConfig
  ) {
    this.logoutRedirectUrl = `${this.config.loginRedirectUrl}/${AuthRoutes.routePath(AuthRoutes.PHSA)}`;
  }

  public ngOnInit(): void {
    this.dashboardMenuItems = this.getDashboardMenuItems();
  }

  private getDashboardMenuItems(): Observable<DashboardMenuItem[]> {
    return of([
      new DashboardRouteMenuItem('PHSA Registration', PhsaEformsRoutes.PHSA_EFORMS, 'people', false, {
        active: true,
        disabled: true
      })
    ]);
  }
}
