import { KeycloakOptions } from 'keycloak-angular';
import { ConfigMap } from '@env/config-map.model';

export class AppEnvironment extends ConfigMap {
  production: boolean;
  version: string;
  environmentName: 'prod' | 'test' | 'dev' | 'local';
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
