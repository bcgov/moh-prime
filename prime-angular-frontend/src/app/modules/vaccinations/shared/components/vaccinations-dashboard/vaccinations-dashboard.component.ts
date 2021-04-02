import { Component, Inject, OnInit } from '@angular/core';

import { Observable, of } from 'rxjs';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { DashboardMenuItem, DashboardRouteMenuItem } from '@lib/modules/dashboard/models/dashboard-menu-item.model';
import { DashboardHeaderConfig } from '@lib/modules/dashboard/components/dashboard-header/dashboard-header.component';
import { VaccinationsRoutes } from '@vaccinations/vaccinations.routes';

@Component({
  selector: 'app-vaccinations-dashboard',
  templateUrl: './vaccinations-dashboard.component.html',
  styleUrls: ['./vaccinations-dashboard.component.scss']
})
export class VaccinationsDashboardComponent implements OnInit {
  public dashboardHeaderConfig: DashboardHeaderConfig;
  public dashboardMenuItems: Observable<DashboardMenuItem[]>;
  public dashboardResponsiveMenuItems: boolean;
  public logoutRedirectUrl: string;

  constructor(
    @Inject(APP_CONFIG) protected config: AppConfig
  ) {
    this.dashboardResponsiveMenuItems = false;
    this.logoutRedirectUrl = `${this.config.loginRedirectUrl}/${VaccinationsRoutes.LOGIN_PAGE}`;
  }

  public ngOnInit(): void {
    this.dashboardHeaderConfig = {
      theme: 'white',
      showMobileToggle: false
    };
    this.dashboardMenuItems = this.getDashboardMenuItems();
  }

  private getDashboardMenuItems(): Observable<DashboardMenuItem[]> {
    return of([
      new DashboardRouteMenuItem('Credentials', VaccinationsRoutes.CREDENTIALS),
      new DashboardRouteMenuItem('Issuance', VaccinationsRoutes.ISSUANCE)
    ]);
  }
}
