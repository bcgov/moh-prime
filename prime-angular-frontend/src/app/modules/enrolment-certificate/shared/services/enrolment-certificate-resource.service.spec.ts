import { TestBed } from '@angular/core/testing';

import { EnrolmentCertificateResource } from './enrolment-certificate-resource.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';

describe('EnrolmentCertificateService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [
      HttpClientTestingModule
    ],
    providers: [
      {
        provide: APP_CONFIG,
        useValue: APP_DI_CONFIG
      }
    ]
  }));

  it('should be created', () => {
    const service: EnrolmentCertificateResource = TestBed.get(EnrolmentCertificateResource);
    expect(service).toBeTruthy();
  });
});
