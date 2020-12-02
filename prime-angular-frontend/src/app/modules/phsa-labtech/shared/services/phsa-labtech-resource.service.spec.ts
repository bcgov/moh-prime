import { TestBed } from '@angular/core/testing';

import { PhsaLabtechResource } from './phsa-labtech-resource.service';

describe('PhsaLabtechResource', () => {
  let service: PhsaLabtechResource;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PhsaLabtechResource);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
