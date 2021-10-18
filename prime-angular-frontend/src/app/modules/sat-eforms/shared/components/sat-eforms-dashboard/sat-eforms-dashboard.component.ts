import { Component, Inject, OnInit } from '@angular/core';

import { Observable, of } from 'rxjs';

import { DashboardMenuItem } from '@lib/modules/dashboard/models/dashboard-menu-item.model';
import { IDashboard } from '@lib/modules/dashboard/interfaces/dashboard.interface';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { SatEformsRoutes } from '@sat/sat-eforms.routes';

@Component({
  selector: 'app-sat-eforms-dashboard',
  templateUrl: './sat-eforms-dashboard.component.html',
  styleUrls: ['./sat-eforms-dashboard.component.scss']
})
export class SatEformsDashboardComponent implements OnInit, IDashboard {
  public dashboardMenuItems: Observable<DashboardMenuItem[]>;
  public logoutRedirectUrl: string;
  public showBrand: boolean;

  constructor(
    @Inject(APP_CONFIG) protected config: AppConfig
  ) {
    this.logoutRedirectUrl = `${ this.config.loginRedirectUrl }/${ SatEformsRoutes.LOGIN_PAGE }`;
    this.showBrand = false;
  }

  public ngOnInit(): void {
    this.dashboardMenuItems = this.getDashboardMenuItems();
  }

  private getDashboardMenuItems(): Observable<DashboardMenuItem[]> {
    return of([]);
  }
}
