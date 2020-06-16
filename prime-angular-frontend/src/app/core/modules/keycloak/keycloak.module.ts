import { NgModule, APP_INITIALIZER, Injector } from '@angular/core';
import { Router } from '@angular/router';

import { JwtHelperService } from '@auth0/angular-jwt';
import { KeycloakAngularModule, KeycloakService, KeycloakOptions } from 'keycloak-angular';

import { ToastService } from '@core/services/toast.service';
import { AuthRoutes } from '@auth/auth.routes';

export class KeycloakOptionsService {
  private path: string;
  private token: string;
  private jwtHelper: JwtHelperService;
  private readonly pathToClientIdMapping = {
    info: 'prime-application-enrolment',
    admin: 'prime-application-admin',
    site: 'prime-application-site'
  };
  private defaultClientId: string;

  constructor() {
    this.path = window.location.pathname.slice(1);
    this.token = localStorage.token;
    this.jwtHelper = new JwtHelperService();

    this.defaultClientId = this.pathToClientIdMapping.info;
  }

  public getKeycloakOptions(): KeycloakOptions {
    const clientId = this.getClientId();

    // TODO split out into readonly member variable
    return {
      config: {
        url: 'https://sso-dev.pathfinder.gov.bc.ca/auth',
        realm: 'v4mbqqas',
        clientId
      },
      initOptions: {
        onLoad: 'check-sso'
      },
      bearerExcludedUrls: ['/provisioner-access/certificate']
    };
  }

  private getClientId(): string {
    let clientId = null;

    if (this.token) {
      clientId = this.getClientIdByToken();
    }

    if (!clientId) {
      clientId = this.getClientIdByPath();
    }

    console.log('CLIENT_ID', clientId);

    return clientId;
  }

  /**
   * @description
   * Attempt to determine the client ID directly from the token.
   */
  private getClientIdByToken() {
    return this.jwtHelper.decodeToken(this.token)?.aud;
  }

  /**
   * @description
   * Attempt to determine the client ID from the path. The paths
   * only include views used for authentication.
   */
  private getClientIdByPath(): string {
    return this.pathToClientIdMapping[this.path] ?? this.defaultClientId;
  }
}

export function initializer(keycloak: KeycloakService, injector: Injector): () => Promise<any> {
  return async (): Promise<boolean | void> => {
    // TODO comment the how/why for dynamically getting the clientId
    const service = new KeycloakOptionsService();
    return keycloak.init(service.getKeycloakOptions())
      .then((authenticated) => {
        const kc = keycloak.getKeycloakInstance();
        kc.onTokenExpired = () => {
          keycloak.updateToken()
            .catch(() => {
              injector.get(ToastService).openErrorToast('Your session has expired, please log in again');
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
