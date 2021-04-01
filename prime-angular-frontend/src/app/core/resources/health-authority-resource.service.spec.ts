import { TestBed } from '@angular/core/testing';

import { HealthAuthorityResourceService } from './health-authority-resource.service';

describe('HealthAuthorityResourceService', () => {
  let service: HealthAuthorityResourceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(HealthAuthorityResourceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
