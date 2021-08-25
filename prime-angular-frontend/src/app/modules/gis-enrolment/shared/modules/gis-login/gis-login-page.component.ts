import { Component, Inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { DashboardHeaderConfig } from '@lib/modules/dashboard/components/dashboard-header/dashboard-header.component';
import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { IdentityProviderEnum } from '@auth/shared/enum/identity-provider.enum';
import { AuthService } from '@auth/shared/services/auth.service';
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
    private router: Router,
    @Inject(APP_CONFIG) private config: AppConfig,
    private authService: AuthService
  ) {
    this.title = route.snapshot.data.title;
    this.dashboardHeaderConfig = { theme: 'white' };
  }

  public onLogin() {
    // Route to COLLECTION_NOTICE which determines the direction of routing
    const redirectRoute = GisEnrolmentRoutes.routePath(GisEnrolmentRoutes.COLLECTION_NOTICE);
    const redirectUri = `${this.config.loginRedirectUrl}${redirectRoute}`;

    this.authService.login({
      idpHint: IdentityProviderEnum.PHSA,
      redirectUri
    });
  }

  public ngOnInit(): void { }
}
