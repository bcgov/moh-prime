import { TestBed } from '@angular/core/testing';

import { HealthAuthSiteRegGuard } from './health-auth-site-reg.guard';

describe('HealthAuthSiteRegGuard', () => {
  let guard: HealthAuthSiteRegGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(HealthAuthSiteRegGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
