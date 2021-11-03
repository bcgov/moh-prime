import { TestBed } from '@angular/core/testing';

import { HealthAuthoritySiteFormStateService } from './health-authority-site-form-state.service';

describe('HealthAuthoritySiteFormStateService', () => {
  let service: HealthAuthoritySiteFormStateService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(HealthAuthoritySiteFormStateService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
