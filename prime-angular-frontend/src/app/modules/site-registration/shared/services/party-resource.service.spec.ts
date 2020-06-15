import { TestBed } from '@angular/core/testing';

import { PartyResource } from './party-resource.service';

describe('PartyResource', () => {
  let service: PartyResource;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PartyResource);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
