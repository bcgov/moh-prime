import { Component, OnInit } from '@angular/core';
import { Inject } from '@angular/core';

import { takeUntil } from 'rxjs/operators';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { AbstractComponent } from '@lib/classes/abstract-component.class';
import { ViewportService } from '@core/services/viewport.service';
import { IdentityProviderEnum } from '@auth/shared/enum/identity-provider.enum';
import { AuthService } from '@auth/shared/services/auth.service';
import { SiteRoutes } from '@registration/site-registration.routes';
import { BannerLocationCode } from '@shared/enums/banner-location-code.enum';

@Component({
  selector: 'app-site',
  templateUrl: './site.component.html',
  styleUrls: [
    './site.component.scss',
    '../../shared/styles/landing-page.scss'
  ]
})
export class SiteComponent extends AbstractComponent implements OnInit {
  public locationCode: BannerLocationCode;
  constructor(
    @Inject(APP_CONFIG) private config: AppConfig,
    private authService: AuthService,
    private viewportService: ViewportService
  ) {
    super();
    this.locationCode = BannerLocationCode.SITE_REGISTRATION_LANDING_PAGE;
  }

  public get isMobile(): boolean {
    return this.viewportService.isMobile;
  }

  public loginUsingBCSC() {
    // Send the user to COLLECTION_NOTICE which determines the direction of routing
    const redirectRoute = SiteRoutes.routePath(SiteRoutes.COLLECTION_NOTICE);
    const redirectUri = `${this.config.loginRedirectUrl}${redirectRoute}`;

    this.authService.login({
      idpHint: IdentityProviderEnum.BCSC,
      redirectUri
    });
  }

  public ngOnInit() {
    this.viewportService.onResize()
      .pipe(takeUntil(this.componentDestroyed$))
      .subscribe();
  }
}
