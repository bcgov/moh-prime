import { TestBed } from '@angular/core/testing';

import { HealthAuthSiteRegResource } from './health-auth-site-reg-resource.service';

describe('HealthAuthSiteRegResourceService', () => {
  let service: HealthAuthSiteRegResource;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(HealthAuthSiteRegResource);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
