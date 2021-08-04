import { Inject, Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Location } from '@angular/common';

import { KeycloakService } from 'keycloak-angular';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { ToastService } from '@core/services/toast.service';
import { AuthRoutes } from '@auth/auth.routes';
import { GisEnrolmentRoutes } from '@gis/gis-enrolment.routes';

@Injectable({
  providedIn: 'root'
})
export class KeycloakInitService {
  constructor(
    @Inject(APP_CONFIG) private config: AppConfig,
    private router: Router,
    private location: Location,
    private keycloakService: KeycloakService,
    private toastService: ToastService
  ) { }

  public async load() {
    const authenticated = await this.keycloakService.init(this.getKeycloakOptions());

    this.keycloakService.getKeycloakInstance().onTokenExpired = () => {
      this.keycloakService.updateToken()
        .catch(() => {
          this.toastService.openErrorToast('Your session has expired, you will need to re-authenticate');
          this.router.navigateByUrl(AuthRoutes.MODULE_PATH);
        });
    };

    if (authenticated) {
      // Force refresh to begin expiry timer
      await this.keycloakService.updateToken(-1);
    }
  }

  private getKeycloakOptions() {
    return (this.isMohKeycloak())
      ? this.config.mohKeycloakConfig
      : this.config.keycloakConfig;
  }

  private isMohKeycloak() {
    const baseUri = this.location.path().slice(1).split('/').shift();
    return [GisEnrolmentRoutes.LOGIN_PAGE, GisEnrolmentRoutes.MODULE_PATH].includes(baseUri);
  }
}
