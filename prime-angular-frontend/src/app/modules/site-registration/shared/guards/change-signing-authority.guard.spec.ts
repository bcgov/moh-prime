import { TestBed } from '@angular/core/testing';

import { ChangeSigningAuthorityGuard } from './change-signing-authority.guard';

describe('ChangeSigningAuthorityGuard', () => {
  let guard: ChangeSigningAuthorityGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(ChangeSigningAuthorityGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
