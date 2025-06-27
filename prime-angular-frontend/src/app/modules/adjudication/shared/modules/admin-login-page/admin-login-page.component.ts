import { Component, OnInit, Inject } from '@angular/core';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { IdentityProviderEnum } from '@auth/shared/enum/identity-provider.enum';
import { AuthService } from '@auth/shared/services/auth.service';

@Component({
  selector: 'app-admin-login-page',
  templateUrl: './admin-login-page.component.html',
  styleUrls: ['./admin-login-page.component.scss']
})
export class AdminLoginPageComponent implements OnInit {
  constructor(
    @Inject(APP_CONFIG) private config: AppConfig,
    private authService: AuthService
  ) { }

  public onLogin() {
    this.authService.login({
      idpHint: IdentityProviderEnum.IDIR_AAD,
      redirectUri: this.config.loginRedirectUrl
    });
  }

  public ngOnInit(): void { }
}
