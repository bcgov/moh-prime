import { NgModule, APP_INITIALIZER, Injector } from '@angular/core';
import { environment } from '@env/environment';

import { KeycloakAngularModule, KeycloakService, KeycloakOptions } from 'keycloak-angular';
import { Router } from '@angular/router';

export function initializer(keycloak: KeycloakService, injector: Injector): () => Promise<any> {
  return (): Promise<any> => {
    console.log(`before :${keycloak.getKeycloakInstance()}`);
    return keycloak.init(environment.keycloakConfig as KeycloakOptions)
      .then(() => {
        console.log('----in then');
        console.log(`after :${keycloak.getKeycloakInstance()}`);
        const kc = keycloak.getKeycloakInstance();
        console.log(kc);
        kc.onTokenExpired = () => {
          keycloak.updateToken()
            .then(() => console.log('-------Successfully refreshed access token.'))
            .catch(() => {
              console.log('---------Failed to refresh access token.');
              injector.get(Router).navigate([environment.loginRedirectUrl]);
            });
        };
      })
      .catch(() => {
        console.log('-----failed to initialize');
        injector.get(Router).navigate([environment.loginRedirectUrl]);
      });
  };
}

// {
//   const initPromise = keycloak.init(environment.keycloakConfig as KeycloakOptions);
//   initPromise.then(() => {
//     keycloak.getKeycloakInstance().onTokenExpired = () => {
//       keycloak.updateToken()
//         .then(() => console.log('Successfully refreshed access token.'))
//         .catch(() => {
//           console.log('Failed to refresh access token.');
//           //router.navigate([environment.loginRedirectUrl]);
//         });
//     };
//   });
//   return initPromise;
// };

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
