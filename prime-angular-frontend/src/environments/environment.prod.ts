export const environment = {
  production: true,
  version: '1.0.0',
  apiEndpoint: '/api/v1',
  loginRedirectUrl: 'http://localhost:4200',
  prime: {
    displayPhone: '1-844-397-7463 (844-39PRIME)',
    phone: '18443977463',
    email: 'prime@gov.bc.ca',
  },
  keycloakConfig: {
    config: {
      url: 'https://sso-dev.pathfinder.gov.bc.ca/auth',
      realm: 'v4mbqqas',
      clientId: 'prime-application-local'
    },
    initOptions: {
      onLoad: 'check-sso'
    },
    bearerExcludedUrls: ['/enrolment-certificates/certificate']
  }
};
