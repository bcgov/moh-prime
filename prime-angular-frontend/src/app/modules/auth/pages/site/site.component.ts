import { Component, OnInit, OnDestroy } from '@angular/core';
import { Inject } from '@angular/core';

import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { ViewportService } from '@core/services/viewport.service';
import { AuthProvider } from '@auth/shared/enum/auth-provider.enum';
import { AuthenticationService } from '@auth/shared/services/authentication.service';
import { SiteRoutes } from '@registration/site-registration.routes';

@Component({
  selector: 'app-site',
  templateUrl: './site.component.html',
  styleUrls: [
    './site.component.scss',
    '../../shared/styles/landing-page.scss'
  ]
})
export class SiteComponent implements OnInit, OnDestroy {
  private unsubscribe$: Subject<void>;

  constructor(
    @Inject(APP_CONFIG) private config: AppConfig,
    private authenticationService: AuthenticationService,
    private viewportService: ViewportService
  ) {
    this.unsubscribe$ = new Subject<void>();
  }

  public get isMobile(): boolean {
    return this.viewportService.isMobile;
  }

  public loginUsingBCSC() {
    // Send the user to COLLECTION_NOTICE which determines the direction of routing
    const redirectRoute = SiteRoutes.routePath(SiteRoutes.COLLECTION_NOTICE);
    const redirectUri = `${this.config.loginRedirectUrl}${redirectRoute}`;

    this.authenticationService.login({
      idpHint: AuthProvider.BCSC,
      redirectUri
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
