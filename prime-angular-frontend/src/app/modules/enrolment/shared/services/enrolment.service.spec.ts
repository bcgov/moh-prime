import { TestBed } from '@angular/core/testing';

import { EnrolmentService } from './enrolment.service';

describe('EnrolmentService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: EnrolmentService = TestBed.get(EnrolmentService);
    expect(service).toBeTruthy();
  });
});
