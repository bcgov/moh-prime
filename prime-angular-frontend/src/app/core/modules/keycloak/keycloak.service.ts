import { KeycloakOptions } from 'keycloak-angular';
import { JwtHelperService } from '@auth0/angular-jwt';

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
