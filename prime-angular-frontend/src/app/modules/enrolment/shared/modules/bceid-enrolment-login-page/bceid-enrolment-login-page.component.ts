import { Component, Inject, OnInit } from '@angular/core';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { IdentityProviderEnum } from '@auth/shared/enum/identity-provider.enum';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from '@auth/shared/services/auth.service';

@Component({
  selector: 'app-bceid-enrolment-login-page',
  templateUrl: './bceid-enrolment-login-page.component.html',
  styleUrls: ['./bceid-enrolment-login-page.component.scss']
})
export class BceidEnrolmentLoginPageComponent implements OnInit {
  public title: string;
  public loginLabel: string;
  public showLogo: boolean;

  constructor(
    private route: ActivatedRoute,
    @Inject(APP_CONFIG) private config: AppConfig,
    private authService: AuthService,
  ) {
    this.title = route.snapshot.data.title;
    this.loginLabel = 'Login using your BCeID';
    this.showLogo = true;
  }

  public onLogin() {
    this.authService.login({
      idpHint: IdentityProviderEnum.BCEID,
      redirectUri: this.config.loginRedirectUrl
    });
  }

  public ngOnInit(): void { }
}
