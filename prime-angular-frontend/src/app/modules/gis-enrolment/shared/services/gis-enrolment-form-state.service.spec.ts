import { TestBed } from '@angular/core/testing';

import { GisEnrolmentFormStateService } from './gis-enrolment-form-state.service';

describe('GisEnrolmentFormStateService', () => {
  let service: GisEnrolmentFormStateService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GisEnrolmentFormStateService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
