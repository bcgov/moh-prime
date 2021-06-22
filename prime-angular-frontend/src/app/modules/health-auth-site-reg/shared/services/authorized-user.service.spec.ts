import { TestBed } from '@angular/core/testing';

import { AuthorizedUserService } from './authorized-user.service';

describe('AuthorizedUserService', () => {
  let service: AuthorizedUserService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AuthorizedUserService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
