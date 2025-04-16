import { KeycloakProfile } from 'keycloak-js';

import { KeycloakAttributes } from '@auth/shared/models/keycloak-attributes.model';

export type BrokerProfile = KeycloakProfile & KeycloakAttributes;
