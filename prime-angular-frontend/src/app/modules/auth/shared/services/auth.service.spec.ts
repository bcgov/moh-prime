import { TestBed } from '@angular/core/testing';

import { KeycloakService } from 'keycloak-angular';

import { MockKeycloakService } from 'test/mocks/mock-keycloak.service';

import { AuthService } from './auth.service';

describe('AuthService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    providers: [
      {
        provide: KeycloakService,
        useClass: MockKeycloakService
      }
    ]
  }));

  it('should be created', () => {
    const service: AuthService = TestBed.get(AuthService);
    expect(service).toBeTruthy();
  });
});
