import { TestBed } from '@angular/core/testing';

import { AuthorizedUserGuard } from './authorized-user.guard';

describe('AuthorizedUserGuard', () => {
  let guard: AuthorizedUserGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(AuthorizedUserGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
