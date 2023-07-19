import { KeycloakTokenParsed } from 'keycloak-js';

import { Role } from '@auth/shared/enum/role.enum';
import { IdentityProviderEnum } from '@auth/shared/enum/identity-provider.enum';

export interface AccessTokenParsed extends KeycloakTokenParsed {
  acr: string;
  address: {
    street_address: string;
    locality: string;
    region: string;
    postal_code: string;
    country: string
  };
  'allowed-origins': string[];
  aud: string;
  auth_time: number;
  azp: string;
  birthdate: string;
  email_verified: boolean;
  family_name: string;
  given_name: string;
  given_names: string;
  identity_assurance_level: number;
  identity_provider: IdentityProviderEnum;
  iss: string;
  jti: string;
  name: string;
  preferred_username: string;
  resource_access?: {
    client: {
      roles: Role[]
    }
  };
  scope: string;
  typ: 'Bearer';
}
