export const environment = {
  production: true,
  version: '1.0.0',
  apiEndpoint: '/api/v1',
  loginRedirectUrl: 'http://localhost:4200',
  documentManagerUrl: 'http://localhost:6001',
  prime: {
    displayPhone: '1-844-39PRIME',
    phone: '1-844-397-7463',
    email: 'prime@gov.bc.ca',
    supportEmail: 'primesupport@gov.bc.ca',
  },
  phoneNumbers: { director: '236-478-0282' },
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
