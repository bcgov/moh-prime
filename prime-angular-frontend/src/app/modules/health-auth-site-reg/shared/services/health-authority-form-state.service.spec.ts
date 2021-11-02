import { TestBed } from '@angular/core/testing';

import { HealthAuthorityFormStateService } from './health-authority-form-state.service';

describe('HealthAuthorityFormStateService', () => {
  let service: HealthAuthorityFormStateService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(HealthAuthorityFormStateService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
