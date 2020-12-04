import { TestBed } from '@angular/core/testing';

import { PhsaLabtechGuard } from './phsa-labtech.guard';

describe('PhsaLabtechGuard', () => {
  let guard: PhsaLabtechGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(PhsaLabtechGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
