import { TestBed } from '@angular/core/testing';

import { GpidResource } from './gpid-resource.service';

describe('GpidResource', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: GpidResource = TestBed.get(GpidResource);
    expect(service).toBeTruthy();
  });
});
