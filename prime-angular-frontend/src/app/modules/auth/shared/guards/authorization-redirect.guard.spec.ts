import { TestBed, async, inject } from '@angular/core/testing';

import { AuthorizationRedirectGuard } from './authorization-redirect.guard';

describe('AuthorizationRedirectGuard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [AuthorizationRedirectGuard]
    });
  });

  it('should ...', inject([AuthorizationRedirectGuard], (guard: AuthorizationRedirectGuard) => {
    expect(guard).toBeTruthy();
  }));
});
