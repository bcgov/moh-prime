import { TestBed } from '@angular/core/testing';

import { AddressAutocompleteResource } from './address-autocomplete-resource.service';

describe('AddressAutocompleteResource', () => {
  let service: AddressAutocompleteResource;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AddressAutocompleteResource);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
