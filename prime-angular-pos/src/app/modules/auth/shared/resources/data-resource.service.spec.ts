import { TestBed } from '@angular/core/testing';

import { DataResource } from './data-resource.service';

describe('DataResource', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: DataResource = TestBed.get(DataResource);
    expect(service).toBeTruthy();
  });
});
