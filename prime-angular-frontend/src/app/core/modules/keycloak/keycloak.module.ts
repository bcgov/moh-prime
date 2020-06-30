import { NgModule, APP_INITIALIZER, Injector } from '@angular/core';
import { Router } from '@angular/router';

import { KeycloakAngularModule, KeycloakService } from 'keycloak-angular';

import { ToastService } from '@core/services/toast.service';
import { AuthRoutes } from '@auth/auth.routes';

import { KeycloakOptionsService } from './keycloak.service';

export function initializer(keycloak: KeycloakService, injector: Injector): () => Promise<any> {
  return async (): Promise<boolean | void> => {
    /**
     * Keycloak clients need to be dynamically determined based on one of two critera:
     * 1) User is unauthenticated and it is based on route
     * 2) User is authenticated and it is based on audience within their token claim
     */
    const service = new KeycloakOptionsService();
    return keycloak.init(service.getKeycloakOptions())
      .then((authenticated) => {
        const kc = keycloak.getKeycloakInstance();
        kc.onTokenExpired = () => {
          keycloak.updateToken()
            .catch(() => {
              injector.get(ToastService).openErrorToast('Your session has expired, please log in again.');
              injector.get(Router).navigateByUrl(AuthRoutes.INFO);
            });
        };

        if (authenticated) {
          // Force refresh to begin expiry timer.
          keycloak.updateToken(-1);
        }
      });
  };
}

@NgModule({
  imports: [KeycloakAngularModule],
  providers: [
    {
      provide: APP_INITIALIZER,
      useFactory: initializer,
      multi: true,
      deps: [KeycloakService, Injector]
    }
  ]
})
export class KeycloakModule { }
