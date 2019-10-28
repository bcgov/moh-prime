import { TestBed, async, inject } from '@angular/core/testing';

import { EnrolleeGuard } from './enrollee.guard';

describe('EnrolleeGuard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [EnrolleeGuard]
    });
  });

  it('should ...', inject([EnrolleeGuard], (guard: EnrolleeGuard) => {
    expect(guard).toBeTruthy();
  }));
});
