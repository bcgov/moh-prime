import { TestBed } from '@angular/core/testing';

import { EnrolleeUtilsService } from './enrollee-utils.service';

describe('EnrolleeUtilsService', () => {
  let service: EnrolleeUtilsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EnrolleeUtilsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
