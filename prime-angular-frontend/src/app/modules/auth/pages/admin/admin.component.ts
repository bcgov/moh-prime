import { Component, OnInit } from '@angular/core';
import { Inject } from '@angular/core';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { AuthProvider } from '@auth/shared/enum/auth-provider.enum';
import { AuthenticationService } from '@auth/shared/services/authentication.service';

@Component({
  selector: 'app-info',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss']
})
export class AdminComponent implements OnInit {
  constructor(
    @Inject(APP_CONFIG) private config: AppConfig,
    private authenticationService: AuthenticationService
  ) { }

  public loginUsingIDIR() {
    this.authenticationService.login({
      idpHint: AuthProvider.IDIR,
      redirectUri: this.config.loginRedirectUrl
    });
  }

  public ngOnInit() { }
}
