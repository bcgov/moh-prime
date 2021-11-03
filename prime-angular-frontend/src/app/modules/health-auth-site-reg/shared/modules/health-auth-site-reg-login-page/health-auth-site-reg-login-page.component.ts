import { Component, Inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { IdentityProviderEnum } from '@auth/shared/enum/identity-provider.enum';
import { AuthService } from '@auth/shared/services/auth.service';

import { HealthAuthSiteRegRoutes } from '@health-auth/health-auth-site-reg.routes';
import { SiteRegistrationTypeEnum } from '@health-auth/shared/enums/site-registration-type.enum';

@Component({
  selector: 'app-health-auth-site-reg-login-page',
  templateUrl: './health-auth-site-reg-login-page.component.html',
  styleUrls: ['./health-auth-site-reg-login-page.component.scss']
})
export class HealthAuthSiteRegLoginPageComponent implements OnInit {
  public title: string;
  public disableLogin: boolean;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    @Inject(APP_CONFIG) private config: AppConfig,
    private authService: AuthService
  ) {
    this.title = route.snapshot.data.title;
  }

  public onLogin(type: SiteRegistrationTypeEnum) {
    // Route to COLLECTION_NOTICE which determines the direction of routing
    const redirectRoute = HealthAuthSiteRegRoutes.routePath(HealthAuthSiteRegRoutes.COLLECTION_NOTICE);
    const redirectUri = `${this.config.loginRedirectUrl}${redirectRoute}`;

    this.authService.login({
      idpHint: IdentityProviderEnum.BCSC,
      redirectUri
    });
  }

  public ngOnInit(): void {
    this.disableLogin = this.config.environmentName === 'prod';
  }
}
