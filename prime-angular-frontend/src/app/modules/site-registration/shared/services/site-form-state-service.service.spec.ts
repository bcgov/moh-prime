import { TestBed } from '@angular/core/testing';

import { SiteFormStateService } from './site-form-state-service.service';

describe('SiteFormStateService', () => {
  let service: SiteFormStateService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SiteFormStateService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
