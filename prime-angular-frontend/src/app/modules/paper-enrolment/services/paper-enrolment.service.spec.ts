import { HttpClientTestingModule } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { ConfigService } from '@config/config.service';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { MockConfigService } from 'test/mocks/mock-config.service';

import { PaperEnrolmentService } from './paper-enrolment.service';

describe('PaperEnrolmentService', () => {
  beforeEach(() => TestBed.configureTestingModule(
    {
      imports: [
        HttpClientTestingModule,
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
    const service: PaperEnrolmentService = TestBed.inject(PaperEnrolmentService);
    expect(service).toBeTruthy();
  });
});
