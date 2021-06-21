import { TestBed } from '@angular/core/testing';

import { PaperEnrolmentGuard } from './paper-enrolment.guard';

describe('PaperEnrolmentGuard', () => {
  let guard: PaperEnrolmentGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(PaperEnrolmentGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
