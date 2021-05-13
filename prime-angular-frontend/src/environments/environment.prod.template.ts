export const environment = {
  production: true,
  version: '1.0.0',
  apiEndpoint: '$REDIRECT_URL/api/v1',
  loginRedirectUrl: '$REDIRECT_URL',
  documentManagerUrl: '$REDIRECT_URL/api/docman',
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
  environmentName: '$OC_APP'
};
