import { TestBed } from '@angular/core/testing';
import { provideHttpClientTesting } from '@angular/common/http/testing';

import { EnrolmentResource } from './enrolment-resource.service';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { SharedModule } from '@shared/shared.module';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('EnrolmentResource', () => {
  let service: EnrolmentResource;

  beforeEach(() => {
    TestBed.configureTestingModule({
    imports: [SharedModule],
    providers: [
        {
            provide: APP_CONFIG,
            useValue: APP_DI_CONFIG
        },
        provideHttpClient(withInterceptorsFromDi()),
        provideHttpClientTesting()
    ]
});
    service = TestBed.inject(EnrolmentResource);
  });

  it('should create', () => {
    expect(service).toBeTruthy();
  });
});
