import { NgModule, APP_INITIALIZER, Injector } from '@angular/core';
import { Router } from '@angular/router';

import { KeycloakAngularModule, KeycloakService, KeycloakOptions } from 'keycloak-angular';

import { environment } from '@env/environment';
import { ToastService } from '@core/services/toast.service';
import { AuthRoutes } from '@auth/auth.routes';

function initializer(keycloak: KeycloakService, injector: Injector): () => Promise<void> {
  return async (): Promise<void> => {
    const authenticated = await keycloak.init((environment.keycloakConfig as KeycloakOptions));
    keycloak.getKeycloakInstance().onTokenExpired = () => {
      keycloak.updateToken()
        .catch(() => {
          injector.get(ToastService).openErrorToast('Your session has expired, you will need to re-authenticate');
          injector.get(Router).navigateByUrl(AuthRoutes.MODULE_PATH);
        });
    };

    if (authenticated) {
      // Force refresh to begin expiry timer.
      keycloak.updateToken(-1);
    }
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
