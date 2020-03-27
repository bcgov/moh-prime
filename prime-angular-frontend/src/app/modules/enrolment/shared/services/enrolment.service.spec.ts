import { TestBed } from '@angular/core/testing';

import { EnrolmentService } from './enrolment.service';

describe('EnrolmentService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should create', () => {
    const service: EnrolmentService = TestBed.inject(EnrolmentService);
    expect(service).toBeTruthy();
  });
});
