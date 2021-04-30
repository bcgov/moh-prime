import { Component, Inject, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { IdentityProviderEnum } from '@auth/shared/enum/identity-provider.enum';
import { AuthService } from '@auth/shared/services/auth.service';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { PhsaEformsRoutes } from '@phsa/phsa-eforms.routes';

@Component({
  selector: 'app-phsa-eforms-login-page',
  templateUrl: './phsa-eforms-login-page.component.html',
  styleUrls: ['./phsa-eforms-login-page.component.scss']
})
export class PhsaEformsLoginPageComponent implements OnInit {
  public title: string;
  public loginLabel: string;

  constructor(
    private route: ActivatedRoute,
    @Inject(APP_CONFIG) private config: AppConfig,
    private authService: AuthService,
  ) {
    this.title = route.snapshot.data.title;
    this.loginLabel = 'Log in with mobile BC Services Card';
  }

  public onLogin() {
    // Route to COLLECTION_NOTICE which determines the direction of routing
    const redirectRoute = PhsaEformsRoutes.routePath(PhsaEformsRoutes.COLLECTION_NOTICE);
    const redirectUri = `${ this.config.loginRedirectUrl }${ redirectRoute }`;

    this.authService.login({
      idpHint: IdentityProviderEnum.BCSC,
      redirectUri
    });
  }

  public ngOnInit(): void { }
}
