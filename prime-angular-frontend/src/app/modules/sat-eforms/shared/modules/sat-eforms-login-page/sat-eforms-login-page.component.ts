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
  public subtitle: string;
  public loginLabel: string;
  public bcscMobileSetupUrl: string;
  public disableLogin: boolean;
  public loginCancelled: boolean;
  public bcscSupportUrl: string;
  public satEformsSupportEmail: string;

  constructor(
    private route: ActivatedRoute,
    @Inject(APP_CONFIG) private config: AppConfig,
    private authService: AuthService,
  ) {
    this.title = 'Enrol for access to Special Authority eForms';
    this.subtitle = 'B.C. healthcare professionals enrol here for access to PharmaCare’s Special Authority eForms application';
    this.loginLabel = 'Log in with the BC Services Card app';
    this.bcscMobileSetupUrl = config.bcscMobileSetupUrl;
    this.bcscSupportUrl = config.bcscSupportUrl;
    this.satEformsSupportEmail = config.satEformsSupportEmail;
    this.loginCancelled =
      this.route.snapshot.queryParams.action === 'cancelled';
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
