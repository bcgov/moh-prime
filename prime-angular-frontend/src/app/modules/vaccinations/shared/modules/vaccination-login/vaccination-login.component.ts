import { Component, Inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { DashboardHeaderConfig } from '@lib/modules/dashboard/components/dashboard-header/dashboard-header.component';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { AuthService } from '@auth/shared/services/auth.service';
import { IdentityProviderEnum } from '@auth/shared/enum/identity-provider.enum';

import { VaccinationsRoutes } from '@vaccinations/vaccinations.routes';

@Component({
  selector: 'app-vaccination-login',
  templateUrl: './vaccination-login.component.html',
  styleUrls: ['./vaccination-login.component.scss']
})
export class VaccinationLoginComponent implements OnInit {
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
    const redirectRoute = VaccinationsRoutes.routePath(VaccinationsRoutes.CREDENTIALS);
    const redirectUri = `${ this.config.loginRedirectUrl }${ redirectRoute }`;

    this.authService.login({
      idpHint: IdentityProviderEnum.BCSC,
      redirectUri
    });
  }

  public ngOnInit(): void { }
}
