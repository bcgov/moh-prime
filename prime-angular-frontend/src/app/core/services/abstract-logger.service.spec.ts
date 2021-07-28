import { TestBed } from '@angular/core/testing';

import { AbstractLoggerService } from './abstract-logger.service';

describe('AbstractLoggerService', () => {
  let service: AbstractLoggerService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AbstractLoggerService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
