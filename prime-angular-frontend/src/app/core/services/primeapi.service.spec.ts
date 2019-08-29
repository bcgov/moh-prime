import { TestBed } from '@angular/core/testing';

import { PrimeapiService } from './primeapi.service';

describe('PrimeapiService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: PrimeapiService = TestBed.get(PrimeapiService);
    expect(service).toBeTruthy();
  });
});
