import { TestBed } from '@angular/core/testing';

import { GisResource } from './gis-resource.service';

describe('GisResource', () => {
  let service: GisResource;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GisResource);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
