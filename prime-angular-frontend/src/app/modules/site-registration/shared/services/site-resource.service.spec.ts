import { TestBed } from '@angular/core/testing';

import { SiteResource } from './site-resource.service';

describe('SiteResource', () => {
  let service: SiteResource;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SiteResource);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
