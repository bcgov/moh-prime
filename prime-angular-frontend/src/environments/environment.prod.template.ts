export const environment = {
  production: true,
  version: '1.0.0',
  apiEndpoint: '/api/v1',
  loginRedirectUrl: '$REDIRECT_URL',
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
