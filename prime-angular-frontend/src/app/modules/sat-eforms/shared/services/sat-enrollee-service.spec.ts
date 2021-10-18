import { TestBed } from '@angular/core/testing';

import { SatEnrolleeService } from './sat-enrollee.service';

describe('SatEnrolleeService', () => {
  let service: SatEnrolleeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SatEnrolleeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
