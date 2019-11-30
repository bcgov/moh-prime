import { TestBed, inject } from '@angular/core/testing';

import { LoggerService } from './logger.service';

describe('LoggerService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [LoggerService]
    });
  });

  it('should create', inject([LoggerService], (service: LoggerService) => {
    expect(service).toBeTruthy();
  }));
});
