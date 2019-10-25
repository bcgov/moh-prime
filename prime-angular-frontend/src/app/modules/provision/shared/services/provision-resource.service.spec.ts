import { TestBed } from '@angular/core/testing';

import { ProvisionResource } from './provision-resource.service';

describe('ProvisionResourceService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ProvisionResource = TestBed.get(ProvisionResource);
    expect(service).toBeTruthy();
  });
});
