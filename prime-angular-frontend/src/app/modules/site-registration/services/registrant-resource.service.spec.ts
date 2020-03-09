import { TestBed } from '@angular/core/testing';

import { RegistrantResource } from './registrant-resource.service';

describe('RegistrantResourceService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: RegistrantResource = TestBed.get(RegistrantResource);
    expect(service).toBeTruthy();
  });
});
