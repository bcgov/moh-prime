import { Component, OnInit } from '@angular/core';
import { Inject } from '@angular/core';

import { takeUntil } from 'rxjs/operators';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { AbstractComponent } from '@lib/classes/abstract-component.class';
import { ViewportService } from '@core/services/viewport.service';
import { IdentityProviderEnum } from '@auth/shared/enum/identity-provider.enum';
import { AuthService } from '@auth/shared/services/auth.service';

@Component({
  selector: 'app-bceid',
  templateUrl: './bceid.component.html',
  styleUrls: ['./bceid.component.scss', '../../shared/styles/landing-page.scss']
})
export class BceidComponent extends AbstractComponent implements OnInit {
  constructor(
    @Inject(APP_CONFIG) private config: AppConfig,
    private authService: AuthService,
    private viewportService: ViewportService
  ) {
    super();
  }

  public get isMobile(): boolean {
    return this.viewportService.isMobile;
  }

  public loginUsingBCeID() {
    this.authService.login({
      idpHint: IdentityProviderEnum.BCEID,
      redirectUri: this.config.loginRedirectUrl
    });
  }

  public ngOnInit() {
    this.viewportService.onResize()
      .pipe(takeUntil(this.componentDestroyed$))
      .subscribe();
  }
}
