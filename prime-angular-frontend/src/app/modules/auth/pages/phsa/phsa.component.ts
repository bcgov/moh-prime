import { Component, OnInit } from '@angular/core';
import { Inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { IdentityProviderEnum } from '@auth/shared/enum/identity-provider.enum';
import { AuthService } from '@auth/shared/services/auth.service';
import { PhsaEformsRoutes } from '@phsa/phsa-eforms.routes';

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
    private route: ActivatedRoute
  ) {
    this.title = route.snapshot.data.title;
  }

  public loginUsingBCSC() {
    // Send the user to COLLECTION_NOTICE which determines the direction of routing
    const redirectRoute = PhsaEformsRoutes.routePath(PhsaEformsRoutes.COLLECTION_NOTICE);
    const redirectUri = `${this.config.loginRedirectUrl}${redirectRoute}`;

    this.authService.login({
      idpHint: IdentityProviderEnum.BCSC,
      redirectUri
    });
  }

  public ngOnInit(): void { }
}
