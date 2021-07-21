import { TestBed } from '@angular/core/testing';

import { WebApiLoggerService } from './web-api-logger.service';

describe('HttpLoggerService', () => {
  let service: WebApiLoggerService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(WebApiLoggerService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
