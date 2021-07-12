import { Component, Inject, OnInit } from '@angular/core';

import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';

import { AppConfig, APP_CONFIG } from 'app/app-config.module';
import { IDashboard } from '@lib/modules/dashboard/interfaces/dashboard.interface';
import { DashboardMenuItem } from '@lib/modules/dashboard/models/dashboard-menu-item.model';
import { HttpEnrollee } from '@shared/models/enrolment.model';
import { AdjudicationRoutes } from '@adjudication/adjudication.routes';

import { PaperEnrolmentResource } from '@paper-enrolment/services/paper-enrolment-resource.service';

@Component({
  selector: 'app-paper-enrolment-dashboard',
  templateUrl: './paper-enrolment-dashboard.component.html',
  styleUrls: ['./paper-enrolment-dashboard.component.scss']
})
export class PaperEnrolmentDashboardComponent implements OnInit, IDashboard {
  public dashboardMenuItems: Observable<DashboardMenuItem[]>;
  public logoutRedirectUrl: string;

  constructor(
    @Inject(APP_CONFIG) protected config: AppConfig,
    private paperEnrolmentResource: PaperEnrolmentResource
  ) {
    this.logoutRedirectUrl = `${this.config.loginRedirectUrl}/${AdjudicationRoutes.LOGIN_PAGE}`;
  }

  public ngOnInit(): void {
    this.dashboardMenuItems = this.getDashboardMenuItems();
    this.getMeSomeData();
  }

  private getDashboardMenuItems(): Observable<DashboardMenuItem[]> {
    return of([]);
  }

  public getMeSomeData() {
    this.paperEnrolmentResource.getEnrolleeById(6).subscribe(f => {
      console.log(f);
      console.log("Approved date is", f.approvedDate);
    });
  }
}
