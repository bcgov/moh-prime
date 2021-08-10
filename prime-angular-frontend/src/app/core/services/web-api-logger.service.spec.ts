import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { WebApiLoggerService } from './web-api-logger.service';

describe('WebApiLoggerService', () => {
  let service: WebApiLoggerService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule
      ],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        }
      ]
    });
    service = TestBed.inject(WebApiLoggerService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
