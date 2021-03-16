import { TestBed } from '@angular/core/testing';

import { BannerResourceService } from './banner-resource.service';

describe('BannerResourceService', () => {
  let service: BannerResourceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BannerResourceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
