import { TestBed } from '@angular/core/testing';

import { GisEnrolmentResource } from './gis-enrolment-resource.service';

describe('GisEnrolmentResource', () => {
  let service: GisEnrolmentResource;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GisEnrolmentResource);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
