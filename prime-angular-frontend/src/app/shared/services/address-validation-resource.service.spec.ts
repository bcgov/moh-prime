import { TestBed } from '@angular/core/testing';

import { AddressValidationResource } from './address-validation-resource.service';

describe('AddressValidationResource', () => {
  let service: AddressValidationResource;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AddressValidationResource);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
