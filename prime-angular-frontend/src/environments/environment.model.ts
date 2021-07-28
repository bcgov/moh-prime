import { KeycloakOptions } from 'keycloak-angular';
import { ConfigMap } from '@env/config-map.model';

export type environmentName = 'prod' | 'test' | 'dev' | 'local';

export class AppEnvironment extends ConfigMap {
  production: boolean;
  version: string;
  environmentName: environmentName;
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
