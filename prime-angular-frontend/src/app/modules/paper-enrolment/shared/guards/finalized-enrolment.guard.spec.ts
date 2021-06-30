import { TestBed } from '@angular/core/testing';

import { FinalizedEnrolmentGuard } from './finalized-enrolment.guard';

describe('FinalizedEnrolmentGuard', () => {
  let guard: FinalizedEnrolmentGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(FinalizedEnrolmentGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
