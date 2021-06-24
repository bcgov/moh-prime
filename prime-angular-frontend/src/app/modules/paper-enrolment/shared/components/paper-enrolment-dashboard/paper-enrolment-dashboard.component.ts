import { Component, Inject, OnInit } from '@angular/core';

import { Observable, of } from 'rxjs';

import { IDashboard } from '@lib/modules/dashboard/interfaces/dashboard.interface';
import { DashboardMenuItem } from '@lib/modules/dashboard/models/dashboard-menu-item.model';

import { AppConfig, APP_CONFIG } from 'app/app-config.module';
import { AdjudicationRoutes } from '@adjudication/adjudication.routes';



@Component({
  selector: 'app-paper-enrolment-dashboard',
  templateUrl: './paper-enrolment-dashboard.component.html',
  styleUrls: ['./paper-enrolment-dashboard.component.scss']
})
export class PaperEnrolmentDashboardComponent implements OnInit, IDashboard {
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
    return of([]);
  }
}
