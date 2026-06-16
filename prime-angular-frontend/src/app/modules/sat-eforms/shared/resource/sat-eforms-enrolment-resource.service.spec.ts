import { TestBed } from '@angular/core/testing';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { MatSnackBarModule } from '@angular/material/snack-bar';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { SatEformsEnrolmentResource } from './sat-eforms-enrolment-resource.service';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('SatEformsEnrolmentResource', () => {
  let service: SatEformsEnrolmentResource;

  beforeEach(() => {
    TestBed.configureTestingModule({
    imports: [MatSnackBarModule],
    providers: [
        {
            provide: APP_CONFIG,
            useValue: APP_DI_CONFIG
        },
        provideHttpClient(withInterceptorsFromDi()),
        provideHttpClientTesting()
    ]
});
    service = TestBed.inject(SatEformsEnrolmentResource);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
