import { KeycloakProfile } from 'keycloak-js';

import { KeycloakAttributes } from '@auth/shared/models/keycloak-attributes.model';

export interface BrokerProfile extends KeycloakProfile, KeycloakAttributes { }
