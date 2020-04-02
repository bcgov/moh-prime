import { TestBed } from '@angular/core/testing';
import { KeycloakTokenService } from './keycloak-token.service';
import { MockKeycloakTokenService } from 'test/mocks/mock-keycloak-token.service';

describe('KeycloakTokenService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    providers: [
      {
        provide: KeycloakTokenService,
        useClass: MockKeycloakTokenService
      }
    ]
  }));

  it('should be created', () => {
    const service: KeycloakTokenService = TestBed.inject(KeycloakTokenService);
    expect(service).toBeTruthy();
  });
});
