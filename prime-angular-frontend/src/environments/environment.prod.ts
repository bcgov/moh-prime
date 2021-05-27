import { AppEnvironment } from '@env/environment.model';
import { environment as defaultEnvironment } from './environment.prod.template';

/**
 * @description
 * Production environment populated with the default
 * environment information and appropriate overrides.
 *
 * NOTE: This environment for local development from within
 * a container, and not used within the deployment pipeline.
 */
export const environment: AppEnvironment = {
  ...defaultEnvironment,
  environmentName: 'local',
  loginRedirectUrl: 'http://localhost:4200',
  documentManagerUrl: 'http://localhost:6001',
  keycloakConfig: {
    config: {
      url: 'https://dev.oidc.gov.bc.ca/auth',
      realm: 'v4mbqqas',
      clientId: 'prime-application-local'
    },
    initOptions: {
      onLoad: 'check-sso'
    },
    bearerExcludedUrls: ['/provisioner-access/certificate']
  }
};
