import { NgModule, APP_INITIALIZER, Injector } from '@angular/core';

import { KeycloakAngularModule, KeycloakService } from 'keycloak-angular';

import { KeycloakInitService } from '@core/modules/keycloak/keycloak-init.service';

function initializer(keycloakInitService: KeycloakInitService): () => Promise<void> {
  return async (): Promise<void> => {
    await keycloakInitService.load();
  };
}

@NgModule({
  imports: [KeycloakAngularModule],
  providers: [
    {
      provide: APP_INITIALIZER,
      useFactory: initializer,
      multi: true,
      deps: [KeycloakInitService]
    }
  ]
})
export class KeycloakModule {}
