import { KeycloakOptions } from 'keycloak-angular';

import { KeycloakOptionsConfig } from './keycloak-options.config';

// TODO incorrect token doesn't exist can only use URLs
/**
 * @description
 * Keycloak clients need to be dynamically determined based on one of two critera:
 * 1) User is unauthenticated and it is based on route, or
 * 2) User is authenticated and it is based on the token audience
 */
export class KeycloakOptionsService {
  private path: string;
  private config: KeycloakOptionsConfig;

  constructor() {
    // Angular has not fully bootstrapped so can't use ActivatedRoute
    this.path = window.location.pathname
      .split('/')
      .filter((segment: string) => segment)
      .shift();
    this.config = new KeycloakOptionsConfig();
  }

  public getKeycloakOptions(): KeycloakOptions {
    const clientId = this.getClientIdByPath(this.path);

    console.log('CLIENT', clientId);

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

  /**
   * @description
   * Attempt to determine the client ID from the path. The paths
   * only include views used for authentication.
   */
  private getClientIdByPath(path: string): string {
    const clientId = this.config.clientConfig
      .filter(c => c.routes.includes(path))
      .shift()
      ?.clientId;

    return clientId ?? this.config.defaultClientId;
  }
}
