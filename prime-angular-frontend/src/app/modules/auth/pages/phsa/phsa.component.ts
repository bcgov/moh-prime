import { Component, OnInit } from '@angular/core';
import { Inject } from '@angular/core';
import { ActivatedRoute, Data } from '@angular/router';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { ViewportService } from '@core/services/viewport.service';
import { IdentityProviderEnum } from '@auth/shared/enum/identity-provider.enum';
import { AuthService } from '@auth/shared/services/auth.service';
import { PhsaLabtechRoutes } from '@phsa/phsa-labtech.routes';

@Component({
  selector: 'app-phsa',
  templateUrl: './phsa.component.html',
  styleUrls: [
    './phsa.component.scss',
    '../../shared/styles/landing-page.scss']
})
export class PhsaComponent implements OnInit {
  public title: string;

  constructor(
    @Inject(APP_CONFIG) private config: AppConfig,
    private authService: AuthService,
    private viewportService: ViewportService,
    route: ActivatedRoute
  ) {
    this.title = route.snapshot.data.title;
  }

  public get isMobile(): boolean {
    return this.viewportService.isMobile;
  }

  public loginUsingBCSC() {
    // TODO: redirect route to the access code page in the PHSA Labtech module
    const redirectRoute = PhsaLabtechRoutes.routePath(PhsaLabtechRoutes.EXAMPLE);
    const redirectUri = `${this.config.loginRedirectUrl}${redirectRoute}`;

    this.authService.login({
      idpHint: IdentityProviderEnum.BCSC,
      redirectUri
    });
  }

  public ngOnInit(): void { }
}
