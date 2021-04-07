import { TestBed } from '@angular/core/testing';

import { HealthAuthSiteRegService } from './health-auth-site-reg.service';

describe('HealthAuthSiteRegService', () => {
  let service: HealthAuthSiteRegService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(HealthAuthSiteRegService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
