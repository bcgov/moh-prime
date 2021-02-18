import { Component, Inject, OnInit } from '@angular/core';

import { Observable, of } from 'rxjs';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { DashboardMenuItem } from '@lib/modules/dashboard/models/dashboard-menu-item.model';
import { GisEnrolmentRoutes } from '@gis/gis-enrolment.routes';

@Component({
  selector: 'app-gis-dashboard',
  templateUrl: './gis-dashboard.component.html',
  styleUrls: ['./gis-dashboard.component.scss']
})
export class GisDashboardComponent implements OnInit {
  public dashboardMenuItems: Observable<DashboardMenuItem[]>;
  public logoutRedirectUrl: string;

  constructor(
    @Inject(APP_CONFIG) protected config: AppConfig
  ) {
    this.logoutRedirectUrl = `${this.config.loginRedirectUrl}/${GisEnrolmentRoutes.LOGIN_PAGE}`;
  }

  public ngOnInit(): void {
    this.dashboardMenuItems = this.getDashboardMenuItems();
  }

  private getDashboardMenuItems(): Observable<DashboardMenuItem[]> {
    return of([]);
  }
}
