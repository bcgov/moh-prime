import { TestBed } from '@angular/core/testing';

import { MohKeycloakInitGuard } from './moh-keycloak-init.guard';

describe('MohKeycloakInitGuard', () => {
  let guard: MohKeycloakInitGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(MohKeycloakInitGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
