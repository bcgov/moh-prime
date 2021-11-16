import { HttpClientTestingModule } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { MatSnackBarModule } from '@angular/material/snack-bar';

import { MockConfigService } from 'test/mocks/mock-config.service';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { ConfigService } from '@config/config.service';
import { EnrolmentService } from './enrolment.service';

describe('EnrolmentService', () => {
  beforeEach(() => TestBed.configureTestingModule(
    {
      imports: [
        HttpClientTestingModule,
        MatSnackBarModule
      ],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        },
        {
          provide: ConfigService,
          useClass: MockConfigService
        }
      ]
    }
  ));

  it('should create', () => {
    const service: EnrolmentService = TestBed.inject(EnrolmentService);
    expect(service).toBeTruthy();
  });
});
