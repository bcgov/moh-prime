import { TestBed } from '@angular/core/testing';

import { GisEnrolmentGuard } from './gis-enrolment.guard';

describe('GisEnrolmentGuard', () => {
  let guard: GisEnrolmentGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(GisEnrolmentGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
