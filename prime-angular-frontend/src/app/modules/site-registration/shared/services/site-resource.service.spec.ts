import { TestBed } from '@angular/core/testing';

import { SiteResourceService } from './site-resource.service';

describe('SiteResourceService', () => {
  let service: SiteResourceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SiteResourceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
