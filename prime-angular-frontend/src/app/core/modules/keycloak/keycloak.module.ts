import { NgModule, APP_INITIALIZER } from '@angular/core';
import { environment } from '@env/environment';

import { KeycloakAngularModule, KeycloakOptions } from 'keycloak-angular';

import { AuthService } from '@auth/shared/services/auth.service';

export function initializer(authService: AuthService): () => Promise<boolean> {
  return (): Promise<boolean> => authService.init(environment.keycloakConfig as KeycloakOptions);
}

@NgModule({
  imports: [KeycloakAngularModule],
  providers: [
    {
      provide: APP_INITIALIZER,
      useFactory: initializer,
      multi: true,
      deps: [AuthService]
    }
  ]
})
export class KeycloakModule { }
