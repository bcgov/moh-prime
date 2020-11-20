import { TestBed } from '@angular/core/testing';

import { PhasLabtechResource } from './phas-labtech-resource.service';

describe('PhasLabtechResource', () => {
  let service: PhasLabtechResource;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PhasLabtechResource);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
