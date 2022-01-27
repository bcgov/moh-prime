import { Component, Inject, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { IdentityProviderEnum } from '@auth/shared/enum/identity-provider.enum';
import { AuthService } from '@auth/shared/services/auth.service';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { SiteRoutes } from '@registration/site-registration.routes';

@Component({
  selector: 'app-site-registration-login-page',
  templateUrl: './site-registration-login-page.component.html',
  styleUrls: ['./site-registration-login-page.component.scss']
})
export class SiteRegistrationLoginPageComponent implements OnInit {
  public title: string;
  private readonly redirectRoutePath: string;

  constructor(
    private route: ActivatedRoute,
    @Inject(APP_CONFIG) private config: AppConfig,
    private authService: AuthService,
  ) {
    this.title = route.snapshot.data.title;
    const redirectRouteSegment = route.snapshot.data.redirectRedirectSegment;
    this.redirectRoutePath = (redirectRouteSegment)
      ? `${redirectRouteSegment}/`
      : ''
  }

  public onLogin() {
    // Route to COLLECTION_NOTICE which determines the direction of routing
    const redirectRoute = SiteRoutes.routePath(`${this.redirectRoutePath}${SiteRoutes.COLLECTION_NOTICE}`);
    const redirectUri = `${this.config.loginRedirectUrl}${redirectRoute}`;

    this.authService.login({
      idpHint: IdentityProviderEnum.BCSC,
      redirectUri
    });
  }

  public ngOnInit(): void { }
}
