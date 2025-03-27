import { AppEnvironment } from '@env/environment.model';

/**
 * @description
 * Production environment populated with the default
 * environment information and appropriate overrides.
 *
 * NOTE: This environment is for local development from
 * within a container, and not used within the deployment
 * pipeline. For pipeline config mapping see main.ts and
 * the AppConfigModule.
 */
export const environment: AppEnvironment = {
  production: true,
  version: '1.0.0',
  environmentName: 'local',
  apiEndpoint: 'http://localhost:5000',
  loginRedirectUrl: 'http://localhost:4200',
  documentManagerUrl: 'http://localhost:6001',
  bcscMobileSetupUrl: 'https://id.gov.bc.ca/account/',
  bcscHelpDeskUrl: 'https://www2.gov.bc.ca/gov/content/governments/government-id/bcservicescardapp/help',
  prime: {
    displayPhone: '1-844-39PRIME',
    phone: '1-844-397-7463',
    email: 'prime@gov.bc.ca',
    supportEmail: 'PRIMESupport@gov.bc.ca'
  },
  phoneNumbers: {
    director: '236-478-0282'
  },
  keycloakConfig: {
    config: {
      url: 'https://common-logon-test.hlth.gov.bc.ca/auth',
      realm: 'moh_applications',
      clientId: 'PRIME-APPLICATION-LOCAL'
    },
    initOptions: {
      onLoad: 'check-sso',
      pkceMethod: 'S256',
      checkLoginIframe: false
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
