import { TestBed, async, inject } from '@angular/core/testing';

import { EnrolmentGuard } from './enrolment.guard';

describe('EnrolmentGuard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [EnrolmentGuard]
    });
  });

  it('should ...', inject([EnrolmentGuard], (guard: EnrolmentGuard) => {
    expect(guard).toBeTruthy();
  }));
});
