import { Component, OnInit, Inject } from '@angular/core';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';

import { AuthProvider } from '@auth/shared/enum/auth-provider.enum';
import { AuthService } from '@auth/shared/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  constructor(
    @Inject(APP_CONFIG) private config: AppConfig,
    private authService: AuthService
  ) { }

  public loginUsingIDIR() {
    this.authService.login({
      idpHint: AuthProvider.BCSC,
      redirectUri: this.config.loginRedirectUrl
    });
  }

  public ngOnInit() { }
}
