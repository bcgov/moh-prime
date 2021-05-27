import { KeycloakOptions } from 'keycloak-angular';

export interface AppEnvironment {
  production: boolean;
  environmentName: string;
  version: string;
  apiEndpoint: string;
  loginRedirectUrl: string;
  documentManagerUrl: string;
  prime: {
    displayPhone: string;
    phone: string;
    email: string;
    supportEmail: string;
  };
  phoneNumbers: {
    director: string;
  };
  keycloakConfig: KeycloakOptions;
  mohKeycloakConfig: KeycloakOptions;
}
