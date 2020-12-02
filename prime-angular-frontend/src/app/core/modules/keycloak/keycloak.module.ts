import { NgModule, APP_INITIALIZER, Injector } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { KeycloakAngularModule, KeycloakService, KeycloakOptions } from 'keycloak-angular';

import { environment } from '@env/environment';
import { ConfigService } from '@config/config.service';
import { ToastService } from '@core/services/toast.service';
import { AuthRoutes } from '@auth/auth.routes';

function initializer(keycloak: KeycloakService, injector: Injector): () => Promise<void> {
  const routeToDefault = () => injector.get(Router).navigateByUrl(AuthRoutes.MODULE_PATH);

  return async (): Promise<void> => {
    const authenticated = await keycloak.init((environment.keycloakConfig as KeycloakOptions));
    keycloak.getKeycloakInstance().onTokenExpired = () => {
      keycloak.updateToken()
        .catch(() => {
          injector.get(ToastService).openErrorToast('Your session has expired, you will need to re-authenticate');
          routeToDefault();
        });
    };

    if (authenticated) {
      // Ensure configuration is populated before the application
      // is fully initialized to prevent race conditions
      await injector.get(ConfigService).load().toPromise();

      // Force refresh to begin expiry timer.
      keycloak.updateToken(-1);
    } else {
      routeToDefault();
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
