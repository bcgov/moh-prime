import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { MatSnackBarModule } from '@angular/material/snack-bar';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { SatEformsEnrolmentResource } from './sat-eforms-enrolment-resource.service';

describe('SatEformsEnrolmentResource', () => {
  let service: SatEformsEnrolmentResource;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        MatSnackBarModule
      ],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        }
      ]
    });
    service = TestBed.inject(SatEformsEnrolmentResource);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
