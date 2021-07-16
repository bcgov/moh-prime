import { KeycloakOptions } from 'keycloak-angular';

export class ConfigMap {
  environmentName: 'prod' | 'test' | 'dev' | 'local';
  apiEndpoint: string;
  loginRedirectUrl: string;
  documentManagerUrl: string;
  keycloakConfig: KeycloakOptions;
}
