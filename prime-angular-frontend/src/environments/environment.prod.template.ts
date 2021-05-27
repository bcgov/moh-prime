import { AppEnvironment } from '@env/environment.model';

/**
 * @description
 * Production environment defaults for replacement from
 * within the OpenShift deployment pipeline.
 *
 * NOTE: Changes should be applied as high in the cascade
 * of the environments to reduce duplication, and to prevent
 * missing environment information during deployments.
 */
export const environment: AppEnvironment = {
  production: true,
  environmentName: '$OC_APP',
  version: '1.0.0',
  apiEndpoint: '/api/v1',
  loginRedirectUrl: '$REDIRECT_URL',
  documentManagerUrl: '$DOCUMENT_MANAGER_URL',
  prime: {
    displayPhone: '1-844-39PRIME',
    phone: '1-844-397-7463',
    email: 'prime@gov.bc.ca',
    supportEmail: 'primesupport@gov.bc.ca',
  },
  phoneNumbers: { director: '236-478-0282' },
  keycloakConfig: {
    config: {
      url: '$KEYCLOAK_URL',
      realm: '$KEYCLOAK_REALM',
      clientId: '$KEYCLOAK_CLIENT_ID'
    },
    initOptions: {
      onLoad: 'check-sso'
    },
    bearerExcludedUrls: ['/provisioner-access/certificate']
  },
  mohKeycloakConfig: {
    config: {
      url: 'https://common-logon-dev.hlth.gov.bc.ca/auth',
      realm: 'moh_applications',
      clientId: 'PRIME-WEBAPP-ENROLLMENT'
    },
    initOptions: {
      onLoad: 'check-sso'
    }
  }
};
