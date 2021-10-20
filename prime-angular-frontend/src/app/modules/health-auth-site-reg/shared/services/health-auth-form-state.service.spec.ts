import { TestBed } from '@angular/core/testing';

import { HealthAuthFormStateService } from './health-auth-form-state.service';

describe('HealthAuthFormStateService', () => {
  let service: HealthAuthFormStateService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(HealthAuthFormStateService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
