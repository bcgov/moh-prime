import { TestBed } from '@angular/core/testing';

import { EnrolmentStateService } from './enrolment-state.service';

describe('EnrolmentStateService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: EnrolmentStateService = TestBed.get(EnrolmentStateService);
    expect(service).toBeTruthy();
  });
});
