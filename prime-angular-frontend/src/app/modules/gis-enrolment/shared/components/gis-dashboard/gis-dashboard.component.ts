import { Component, Inject, OnInit } from '@angular/core';

import { Observable, of } from 'rxjs';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { DashboardMenuItem } from '@lib/modules/dashboard/models/dashboard-menu-item.model';
import { DashboardHeaderConfig } from '@lib/modules/dashboard/components/dashboard-header/dashboard-header.component';
import { GisEnrolmentRoutes } from '@gis/gis-enrolment.routes';

@Component({
  selector: 'app-gis-dashboard',
  templateUrl: './gis-dashboard.component.html',
  styleUrls: ['./gis-dashboard.component.scss']
})
export class GisDashboardComponent implements OnInit {
  public dashboardHeaderConfig: DashboardHeaderConfig;
  public dashboardSideNavConfig: { imgSrc: string, imgAlt: string };
  public dashboardMenuItems: Observable<DashboardMenuItem[]>;
  public dashboardResponsiveMenuItems: boolean;
  public logoutRedirectUrl: string;

  constructor(
    @Inject(APP_CONFIG) protected config: AppConfig
  ) {
    this.dashboardResponsiveMenuItems = false;
    this.logoutRedirectUrl = `${this.config.loginRedirectUrl}/${GisEnrolmentRoutes.LOGIN_PAGE}`;
  }

  public ngOnInit(): void {
    this.dashboardHeaderConfig = {
      theme: 'white',
      showMobileToggle: false
    };
    this.dashboardSideNavConfig = {
      imgSrc: '/assets/gis_brand.jpeg',
      imgAlt: 'GIS Logo'
    };
    this.dashboardMenuItems = this.getDashboardMenuItems();
  }

  private getDashboardMenuItems(): Observable<DashboardMenuItem[]> {
    return of([]);
  }
}
