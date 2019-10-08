import { TestBed } from '@angular/core/testing';

import { EnrolmentResourceService } from './enrolment-resource.service';

describe('EnrolmentResourceService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: EnrolmentResourceService = TestBed.get(EnrolmentResourceService);
    expect(service).toBeTruthy();
  });
});
