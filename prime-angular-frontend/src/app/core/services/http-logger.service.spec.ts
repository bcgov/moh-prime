import { TestBed } from '@angular/core/testing';

import { HttpLoggerService } from './http-logger.service';

describe('HttpLoggerService', () => {
  let service: HttpLoggerService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(HttpLoggerService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
