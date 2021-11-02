import { TestBed } from '@angular/core/testing';

import { HealthAuthoritySiteService } from './health-authority-site.service';

describe('HealthAuthoritySiteService', () => {
  let service: HealthAuthoritySiteService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(HealthAuthoritySiteService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
