import { Component, OnInit } from '@angular/core';
import { Inject } from '@angular/core';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { AuthService } from '@auth/shared/services/auth.service';

@Component({
  selector: 'app-info',
  templateUrl: './info.component.html',
  styleUrls: ['./info.component.scss']
})
export class InfoComponent implements OnInit {

  constructor(
    @Inject(APP_CONFIG) private config: AppConfig,
    private authService: AuthService
  ) { }

  public loginUsingBCSC() {
    this.authService.login({
      idpHint: 'bcsc',
      redirectUri: this.config.loginRedirectUrl
    });
  }

  public loginUsingIDIR() {
    this.authService.login({
      idpHint: 'idir',
      redirectUri: this.config.loginRedirectUrl
    });
  }

  public ngOnInit() { }
}
