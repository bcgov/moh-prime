import { TestBed } from '@angular/core/testing';
import { CanActivateFn } from '@angular/router';

import { IdentityInsuranceLevelGuard } from './identity-insurance-level.guard';

describe('IdentityInsuranceLevelGuard', () => {
  const executeGuard: CanActivateFn = (...guardParameters) =>
    TestBed.runInInjectionContext(() => IdentityInsuranceLevelGuard(...guardParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeGuard).toBeTruthy();
  });
});
