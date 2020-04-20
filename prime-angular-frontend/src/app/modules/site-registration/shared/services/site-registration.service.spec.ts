import { TestBed } from '@angular/core/testing';

import { SiteRegisrationService } from './site-registration.service';

describe('SiteRegisrationService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: SiteRegisrationService = TestBed.inject(SiteRegisrationService);
    expect(service).toBeTruthy();
  });
});
