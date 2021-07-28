import { NgModule, APP_INITIALIZER, Injector } from '@angular/core';

import { KeycloakAngularModule } from 'keycloak-angular';

import { KeycloakInitService } from '@core/modules/keycloak/keycloak-init.service';

function keycloakFactory(keycloakInitService: KeycloakInitService): () => Promise<void> {
  return async () => await keycloakInitService.load();
}

@NgModule({
  imports: [KeycloakAngularModule],
  providers: [
    {
      provide: APP_INITIALIZER,
      useFactory: keycloakFactory,
      multi: true,
      deps: [KeycloakInitService]
    }
  ]
})
export class KeycloakModule {}
