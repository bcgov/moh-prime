import { KeycloakOptions } from 'keycloak-angular';

import { environmentName } from '@env/environment.model';

export class ConfigMap {
  environmentName: environmentName;
  apiEndpoint: string;
  loginRedirectUrl: string;
  documentManagerUrl: string;
  bcscMobileSetupUrl: string;
  keycloakConfig: KeycloakOptions;
  mohKeycloakConfig: KeycloakOptions;
}
