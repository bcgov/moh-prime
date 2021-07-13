import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { PaperEnrolmentResource } from './paper-enrolment-resource.service';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { SharedModule } from '@shared/shared.module';

describe('PaperEnrolmentResource', () => {
  let service: PaperEnrolmentResource;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        SharedModule
      ],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        }
      ]
    });
    service = TestBed.inject(PaperEnrolmentResource);
  });

  it('should create', () => {
    expect(service).toBeTruthy();
  });
});
