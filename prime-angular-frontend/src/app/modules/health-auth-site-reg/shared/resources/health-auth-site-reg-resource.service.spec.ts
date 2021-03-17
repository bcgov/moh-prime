import { TestBed } from '@angular/core/testing';

import { HealthAuthSiteRegResourceService } from './health-auth-site-reg-resource.service';

describe('HealthAuthSiteRegResourceService', () => {
  let service: HealthAuthSiteRegResourceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(HealthAuthSiteRegResourceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
