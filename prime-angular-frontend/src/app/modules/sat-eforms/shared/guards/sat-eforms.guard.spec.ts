import { TestBed } from '@angular/core/testing';

import { SatEformsGuard } from './sat-eforms.guard';

describe('SatEformsGuard', () => {
  let guard: SatEformsGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(SatEformsGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
