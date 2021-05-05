import { TestBed } from '@angular/core/testing';

import { PrimeKeycloakInitGuard } from './prime-keycloak-init.guard';

describe('PrimeKeycloakInitGuard', () => {
  let guard: PrimeKeycloakInitGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(PrimeKeycloakInitGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
