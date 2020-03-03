import { TestBed } from '@angular/core/testing';

import { KeycloakTokenService } from './keycloak-token.service';

describe('KeycloakTokenService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: KeycloakTokenService = TestBed.get(KeycloakTokenService);
    expect(service).toBeTruthy();
  });
});
