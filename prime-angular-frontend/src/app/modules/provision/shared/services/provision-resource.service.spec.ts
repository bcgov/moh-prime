import { TestBed } from '@angular/core/testing';

import { ProvisionResourceService } from './provision-resource.service';

describe('ProvisionResourceService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ProvisionResourceService = TestBed.get(ProvisionResourceService);
    expect(service).toBeTruthy();
  });
});
