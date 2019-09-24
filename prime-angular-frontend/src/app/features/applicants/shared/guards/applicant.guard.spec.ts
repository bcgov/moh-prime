import { TestBed, async, inject } from '@angular/core/testing';

import { ApplicantGuard } from './applicant.guard';

describe('ApplicantGuard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ApplicantGuard]
    });
  });

  it('should ...', inject([ApplicantGuard], (guard: ApplicantGuard) => {
    expect(guard).toBeTruthy();
  }));
});
