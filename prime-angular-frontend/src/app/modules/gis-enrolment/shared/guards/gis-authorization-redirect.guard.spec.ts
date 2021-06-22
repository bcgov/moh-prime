import { TestBed } from '@angular/core/testing';

import { GisAuthorizationRedirectGuard } from './gis-authorization-redirect.guard';

describe('GisAuthorizationRedirectGuard', () => {
  let guard: GisAuthorizationRedirectGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(GisAuthorizationRedirectGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
