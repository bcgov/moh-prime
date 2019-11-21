import { Component, OnInit } from '@angular/core';
import { Inject } from '@angular/core';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { AuthProvider } from '@auth/shared/enum/auth-provider.enum';
import { AuthService } from '@auth/shared/services/auth.service';
import { EnrolmentRoutes } from '@enrolment/enrolent.routes';

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
    const redirectUri = `${this.config.loginRedirectUrl}${EnrolmentRoutes.routePath(EnrolmentRoutes.COLLECTION_NOTICE)}`;

    this.authService.login({
      idpHint: AuthProvider.BCSC,
      redirectUri
    });
  }

  public ngOnInit() { }
}
