import { Component, OnInit } from '@angular/core';
import { Inject } from '@angular/core';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { AuthProvider } from '@auth/shared/enum/auth-provider.enum';
import { AuthService } from '@auth/shared/services/auth.service';

@Component({
  selector: 'app-bceid',
  templateUrl: './bceid.component.html',
  styleUrls: ['./bceid.component.scss']
})
export class BceidComponent implements OnInit {
  constructor(
    @Inject(APP_CONFIG) private config: AppConfig,
    private authService: AuthService
  ) { }

  public loginUsingBCeID() {
    this.authService.login({
      idpHint: AuthProvider.BCEID,
      redirectUri: this.config.loginRedirectUrl
    });
  }

  public ngOnInit() { }
}
