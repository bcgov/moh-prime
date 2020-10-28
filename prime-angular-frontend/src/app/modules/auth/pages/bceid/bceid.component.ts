import { Component, OnInit, OnDestroy } from '@angular/core';
import { Inject } from '@angular/core';

import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { ViewportService } from '@core/services/viewport.service';
import { IdentityProviderEnum } from '@auth/shared/enum/identity-provider.enum';
import { AuthService } from '@auth/shared/services/auth.service';

@Component({
  selector: 'app-bceid',
  templateUrl: './bceid.component.html',
  styleUrls: ['./bceid.component.scss', '../../shared/styles/landing-page.scss']
})
export class BceidComponent implements OnInit, OnDestroy {
  private unsubscribe$: Subject<void>;

  constructor(
    @Inject(APP_CONFIG) private config: AppConfig,
    private authService: AuthService,
    private viewportService: ViewportService
  ) {
    this.unsubscribe$ = new Subject<void>();
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
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe();
  }

  public ngOnDestroy() {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }
}
