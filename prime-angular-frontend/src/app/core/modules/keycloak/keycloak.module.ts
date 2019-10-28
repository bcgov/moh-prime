import { NgModule, APP_INITIALIZER } from '@angular/core';
import { environment } from '@env/environment';

import { KeycloakService, KeycloakAngularModule, KeycloakOptions } from 'keycloak-angular';

export function initializer(keycloak: KeycloakService): () => Promise<boolean> {
  return (): Promise<boolean> => keycloak.init(environment.keycloakConfig as KeycloakOptions);
}

@NgModule({
  imports: [KeycloakAngularModule],
  providers: [
    {
      provide: APP_INITIALIZER,
      useFactory: initializer,
      multi: true,
      deps: [KeycloakService]
    }
  ]
})
export class KeycloakModule { }
