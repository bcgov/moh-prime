import { TestBed } from '@angular/core/testing';

import { SatEformsEnrolmentResource } from './sat-eforms-enrolment-resource.service';

describe('SatEformsEnrolmentResource', () => {
  let service: SatEformsEnrolmentResource;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SatEformsEnrolmentResource);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
