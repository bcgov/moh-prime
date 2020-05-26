import { TestBed } from '@angular/core/testing';

import { OrgBookResource } from './org-book-resource.service';

describe('OrgBookResource', () => {
  let service: OrgBookResource;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(OrgBookResource);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
