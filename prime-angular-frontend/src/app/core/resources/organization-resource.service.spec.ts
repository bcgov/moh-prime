import { TestBed } from '@angular/core/testing';

import { OrganizationResource } from './organization-resource.service';

describe('OrganizationResource', () => {
  let service: OrganizationResource;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(OrganizationResource);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
