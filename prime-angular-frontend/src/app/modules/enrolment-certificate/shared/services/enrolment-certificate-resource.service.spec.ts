import { TestBed } from '@angular/core/testing';

import { EnrolmentCertificateResource } from './enrolment-certificate-resource.service';

describe('EnrolmentCertificateService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: EnrolmentCertificateResource = TestBed.get(EnrolmentCertificateResource);
    expect(service).toBeTruthy();
  });
});
