import { TestBed } from '@angular/core/testing';

import { HealthAuthSiteRegFormStateService } from './health-auth-site-reg-form-state.service';

describe('HealthAuthSiteRegFormStateService', () => {
  let service: HealthAuthSiteRegFormStateService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(HealthAuthSiteRegFormStateService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
