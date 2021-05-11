import { TestBed } from '@angular/core/testing';

import { AuthorizedUserResourceService } from './authorized-user-resource.service';

describe('AuthorizedUserResourceService', () => {
  let service: AuthorizedUserResourceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AuthorizedUserResourceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
