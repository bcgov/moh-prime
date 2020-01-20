import { Component, OnInit } from '@angular/core';
import { Inject } from '@angular/core';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { AuthProvider } from '@auth/shared/enum/auth-provider.enum';
import { AuthService } from '@auth/shared/services/auth.service';

@Component({
  selector: 'app-info',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss']
})
export class AdminComponent implements OnInit {
  constructor(
    @Inject(APP_CONFIG) private config: AppConfig,
    private authService: AuthService
  ) { }

  public loginUsingIDIR() {
    this.authService.login({
      idpHint: AuthProvider.IDIR,
      redirectUri: this.config.loginRedirectUrl
    });
  }

  public ngOnInit() { }
}
