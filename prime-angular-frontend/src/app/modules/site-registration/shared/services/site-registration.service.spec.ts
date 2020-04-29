import { TestBed } from '@angular/core/testing';

import { SiteRegistrationService } from './site-registration.service';

describe('SiteRegistrationService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: SiteRegistrationService = TestBed.inject(SiteRegistrationService);
    expect(service).toBeTruthy();
  });
});
