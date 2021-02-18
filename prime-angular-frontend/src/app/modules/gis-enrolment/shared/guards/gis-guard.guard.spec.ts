import { TestBed } from '@angular/core/testing';

import { GisGuardGuard } from './gis-guard.guard';

describe('GisGuardGuard', () => {
  let guard: GisGuardGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(GisGuardGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
