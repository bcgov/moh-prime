import { TestBed } from '@angular/core/testing';

import { KeycloakService } from 'keycloak-angular';

import { MockKeycloakService } from 'test/mocks/mock-keycloak.service';

import { AuthService } from './auth.service';

describe('AuthService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    providers: [
      {
        provide: KeycloakService,
        useValue: MockKeycloakService
      }
    ]
  }));

  it('should be created', () => {
    const service: AuthService = TestBed.get(AuthService);
    expect(service).toBeTruthy();
  });
});
