import { TestBed } from '@angular/core/testing';

import { GisEnrolmentService } from './gis-enrolment.service';

describe('GisEnrolmentService', () => {
  let service: GisEnrolmentService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GisEnrolmentService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
