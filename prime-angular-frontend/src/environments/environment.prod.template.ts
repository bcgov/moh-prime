export const environment = {
  production: true,
  version: '1.0.0',
  apiEndpoint: '/api/v1',
  loginRedirectUrl: '$REDIRECT_URL',
  prime: {
    displayPhone: '1-844-397-7463 (844-39PRIME)',
    phone: '18443977463',
    email: 'prime@gov.bc.ca',
  },
  keycloakConfig: {
    config: {
      url: '$KEYCLOAK_URL',
      realm: '$KEYCLOAK_REALM',
      clientId: '$KEYCLOAK_CLIENT_ID'
    },
    initOptions: {
      onLoad: 'check-sso'
    }
  }
};
