import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

import { KeycloakOptions, KeycloakService } from 'keycloak-angular';

import { environment } from '@env/environment';
import { ToastService } from '@core/services/toast.service';
import { AuthRoutes } from '@auth/auth.routes';

@Injectable({
  providedIn: 'root'
})
export class KeycloakInitService {
  constructor(
    private router: Router,
    private keycloakService: KeycloakService,
    private toastService: ToastService
  ) {
  }

  public async load() {
    const authenticated = await this.keycloakService.init((environment.keycloakConfig as KeycloakOptions));
    this.keycloakService.getKeycloakInstance().onTokenExpired = () => {
      this.keycloakService.updateToken()
        .catch(() => {
          this.toastService.openErrorToast('Your session has expired, you will need to re-authenticate');
          this.router.navigateByUrl(AuthRoutes.MODULE_PATH);
        });
    };

    if (authenticated) {
      // Force refresh to begin expiry timer.
      await this.keycloakService.updateToken(-1);
    }
  }
}
