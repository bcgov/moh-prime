import { TestBed } from '@angular/core/testing';

import { ConsoleLoggerService } from './console-logger.service';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';

describe('ConsoleLoggerService', () => {
  let service: ConsoleLoggerService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        }
      ]
    });
    service = TestBed.inject(ConsoleLoggerService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
