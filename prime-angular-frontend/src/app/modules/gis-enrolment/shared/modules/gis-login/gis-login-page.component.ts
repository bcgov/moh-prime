import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { DashboardHeaderConfig } from '@lib/modules/dashboard/components/dashboard-header/dashboard-header.component';
import { GisEnrolmentRoutes } from '@gis/gis-enrolment.routes';

@Component({
  selector: 'app-gis-login-page',
  templateUrl: './gis-login-page.component.html',
  styleUrls: ['./gis-login-page.component.scss']
})
export class GisLoginPageComponent implements OnInit {
  public title: string;
  public dashboardHeaderConfig: DashboardHeaderConfig;

  constructor(
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.title = route.snapshot.data.title;
    this.dashboardHeaderConfig = {
      theme: 'white',
      showMobileToggle: true
    };
  }

  public login() {
    this.router.navigate([GisEnrolmentRoutes.MODULE_PATH]);
  }

  public ngOnInit(): void { }
}
