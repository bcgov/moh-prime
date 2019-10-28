export const environment = {
  production: true,
  version: '1.0.0',
  apiEndpoint: '/api/v1',
  loginRedirectUrl: 'http://localhost:4200',
  keycloakConfig: {
    config: {
      url: 'https://sso-dev.pathfinder.gov.bc.ca/auth',
      realm: 'v4mbqqas',
      clientId: 'prime-application-local'
    },
    initOptions: {
      onLoad: 'check-sso'
    }
  }
};
