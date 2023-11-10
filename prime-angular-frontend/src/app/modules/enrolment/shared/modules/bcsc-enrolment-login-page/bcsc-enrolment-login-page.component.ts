import { Component, Inject, OnInit } from '@angular/core';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { IdentityProviderEnum } from '@auth/shared/enum/identity-provider.enum';
import { AuthService } from '@auth/shared/services/auth.service';
import { EnrolmentRoutes } from '@enrolment/enrolment.routes';

@Component({
  selector: 'app-bcsc-enrolment-login-page',
  templateUrl: './bcsc-enrolment-login-page.component.html',
  styleUrls: ['./bcsc-enrolment-login-page.component.scss']
})
export class BcscEnrolmentLoginPageComponent implements OnInit {
  constructor(
    @Inject(APP_CONFIG) private config: AppConfig,
    private authService: AuthService
  ) { }

  public onLogin() {
    // Route to COLLECTION_NOTICE which determines the direction of routing
    //const redirectRoute = EnrolmentRoutes.routePath(EnrolmentRoutes.COLLECTION_NOTICE);

    const redirectRoute = '/site/collection-notice'
    const redirectUri = `${this.config.loginRedirectUrl}${redirectRoute}`;

    this.authService.login({
      idpHint: IdentityProviderEnum.BCSC_MOH,
      redirectUri
    });
  }

  public ngOnInit(): void { }
}
