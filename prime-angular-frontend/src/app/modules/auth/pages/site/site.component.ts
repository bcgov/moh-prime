import { Component, OnInit, Inject } from '@angular/core';
import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { AuthService } from '@auth/shared/services/auth.service';
import { UtilsService } from '@core/services/utils.service';
import { AuthProvider } from '@auth/shared/enum/auth-provider.enum';
import { SiteRoutes } from 'app/modules/site-registration/site-registration.routes';

@Component({
  selector: 'app-site',
  templateUrl: './site.component.html',
  styleUrls: ['./site.component.scss']
})
export class SiteComponent implements OnInit {
  public isIE: boolean;

  constructor(
    @Inject(APP_CONFIG) private config: AppConfig,
    private authService: AuthService,
    private utilsService: UtilsService
  ) {
    this.isIE = this.utilsService.isIE();
  }

  public loginUsingBCSC() {
    const redirectUri = `${this.config.loginRedirectUrl}${SiteRoutes.routePath(SiteRoutes.COLLECTION_NOTICE)}`;

    this.authService.login({
      idpHint: AuthProvider.BCSC,
      redirectUri
    });
  }

  public ngOnInit() { }
}
