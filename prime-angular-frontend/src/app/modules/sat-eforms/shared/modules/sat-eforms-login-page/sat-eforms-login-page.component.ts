import { Component, Inject, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { IdentityProviderEnum } from '@auth/shared/enum/identity-provider.enum';
import { AuthService } from '@auth/shared/services/auth.service';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { SatEformsRoutes } from '@sat/sat-eforms.routes';

@Component({
  selector: 'app-sat-eforms-login-page',
  templateUrl: './sat-eforms-login-page.component.html',
  styleUrls: ['./sat-eforms-login-page.component.scss']
})
export class SatEformsLoginPageComponent implements OnInit {
  public title: string;
  public loginLabel: string;
  public bcscMobileSetupUrl: string;
  public disableLogin: boolean;

  constructor(
    private route: ActivatedRoute,
    @Inject(APP_CONFIG) private config: AppConfig,
    private authService: AuthService,
  ) {
    this.title = 'Enrol for access to Special Authority eForms';
    this.loginLabel = 'Enrol using your BCSC';
    this.bcscMobileSetupUrl = config.bcscMobileSetupUrl;
  }

  public onLogin() {
    if (this.disableLogin) {
      return;
    }

    // Route to COLLECTION_NOTICE which determines the direction of routing
    const redirectRoute = SatEformsRoutes.routePath(SatEformsRoutes.COLLECTION_NOTICE);
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
