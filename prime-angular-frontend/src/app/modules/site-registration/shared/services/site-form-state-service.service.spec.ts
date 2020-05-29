import { TestBed } from '@angular/core/testing';

import { SiteFormStateServiceService } from './site-form-state-service.service';

describe('SiteFormStateServiceService', () => {
  let service: SiteFormStateServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SiteFormStateServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
