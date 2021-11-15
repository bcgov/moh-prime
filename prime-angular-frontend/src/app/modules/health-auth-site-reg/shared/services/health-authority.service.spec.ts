import { TestBed } from '@angular/core/testing';

import { HealthAuthorityService } from './health-authority.service';

describe('HealthAuthorityService', () => {
  let service: HealthAuthorityService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(HealthAuthorityService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
