import { Component, OnInit, Inject } from '@angular/core';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';

import { AuthProvider } from '@auth/shared/enum/auth-provider.enum';
import { AuthService } from '@auth/shared/services/auth.service';
import { DataResource } from '@auth/shared/resources/data-resource.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  constructor(
    @Inject(APP_CONFIG) private config: AppConfig,
    private authService: AuthService,
    private dataResource: DataResource
  ) { }

  public login() {
    this.authService.login({
      idpHint: AuthProvider.BCSC,
      redirectUri: this.config.loginRedirectUrl
    });
  }

  public ngOnInit() { }
}
