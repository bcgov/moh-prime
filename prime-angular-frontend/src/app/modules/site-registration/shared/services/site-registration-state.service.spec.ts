import { TestBed } from '@angular/core/testing';

import { SiteRegistrationStateService } from './site-registration-state.service';

describe('SiteRegistrationStateService', () => {
  let service: SiteRegistrationStateService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SiteRegistrationStateService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
