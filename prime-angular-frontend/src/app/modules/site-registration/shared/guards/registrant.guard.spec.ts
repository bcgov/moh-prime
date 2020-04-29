import { TestBed } from '@angular/core/testing';

import { RegistrantGuard } from './registrant.guard';

describe('RegistrantGuard', () => {
  let guard: RegistrantGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(RegistrantGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
