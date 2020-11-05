import { TestBed } from '@angular/core/testing';

import { EnrolmentHelpersService } from './enrolment-helpers.service';

describe('EnrolmentHelpersService', () => {
  let service: EnrolmentHelpersService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EnrolmentHelpersService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
