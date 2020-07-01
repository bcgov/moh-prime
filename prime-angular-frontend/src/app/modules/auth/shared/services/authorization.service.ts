import { Injectable } from '@angular/core';

import { Role } from '../enum/role.enum';
import { AuthenticationService } from './authentication.service';
import { KeycloakTokenService } from './keycloak-token.service';

@Injectable({
  providedIn: 'root'
})
export class AuthorizationService {
  constructor(
    private authenticationService: AuthenticationService,
    private keycloakTokenService: KeycloakTokenService
  ) { }
}
