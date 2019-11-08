import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { EnrolmentResource } from './enrolment-resource.service';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';

describe('EnrolmentResource', () => {
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

  it('should create', () => {
    const service: EnrolmentResource = TestBed.get(EnrolmentResource);
    expect(service).toBeTruthy();
  });
});
