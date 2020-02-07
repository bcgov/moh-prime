import { NgModule, APP_INITIALIZER, Injector } from '@angular/core';
import { environment } from '@env/environment';

import { KeycloakAngularModule, KeycloakService, KeycloakOptions } from 'keycloak-angular';
import { Router } from '@angular/router';
import { ToastService } from '@core/services/toast.service';
import { AuthRoutes } from '@auth/auth.routes';

export function initializer(keycloak: KeycloakService, injector: Injector): () => Promise<any> {
  return (): Promise<any> => {
    return keycloak.init(environment.keycloakConfig as KeycloakOptions)
      .then(
        (authenticated) => {
          const kc = keycloak.getKeycloakInstance();
          kc.onTokenExpired = () => {
            keycloak.updateToken()
              .catch(() => redirectToLogin(injector));
          };

          if (authenticated) {
            // Force refresh to begin expiry timer.
            keycloak.updateToken(-1);
          }
        },
        (error: any) => redirectToLogin(injector)
      );
  };
}

function redirectToLogin(injector: Injector): void {
  injector.get(ToastService).openErrorToast('Your session has expired, please log in again');
  injector.get(Router).navigateByUrl(AuthRoutes.INFO);
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
