import { TestBed } from '@angular/core/testing';

import { ConsoleLoggerService } from './console-logger.service';

describe('ConsoleLoggerService', () => {
  let service: ConsoleLoggerService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ConsoleLoggerService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
