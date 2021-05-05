import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { AuthRoutes } from '@auth/auth.routes';
import { ConfigService } from '@config/config.service';
import { KeycloakOptions, KeycloakService } from 'keycloak-angular';
import { ToastService } from './toast.service';

@Injectable({
  providedIn: 'root'
})
export class KeycloakUtilsService {

  constructor(
    private router: Router,
    private keycloak: KeycloakService,
    private toastService: ToastService,
    private configService: ConfigService,
  ) { }

  public async initialize(config: KeycloakOptions): Promise<boolean> {
    const routeToDefault = () => this.router.navigateByUrl(AuthRoutes.MODULE_PATH);

    const authenticated = await this.keycloak.init(config);
    this.keycloak.getKeycloakInstance().onTokenExpired = () => {
      this.keycloak.updateToken()
        .catch(() => {
          this.toastService.openErrorToast('Your session has expired, you will need to re-authenticate');
          routeToDefault();
        });
    };

    // if (authenticated) {
    //   // Ensure configuration is populated before the application
    //   // is fully initialized to prevent race conditions
    //   await this.configService.load().toPromise();

    //   // Force refresh to begin expiry timer.
    //   this.keycloak.updateToken(-1);
    // }
    return true;
  }
}
