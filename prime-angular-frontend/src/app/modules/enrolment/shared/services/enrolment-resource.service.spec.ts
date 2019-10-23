import { TestBed } from '@angular/core/testing';

import { EnrolmentResource } from './enrolment-resource.service';

describe('EnrolmentResource', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: EnrolmentResource = TestBed.get(EnrolmentResource);
    expect(service).toBeTruthy();
  });
});
