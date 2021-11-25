import { TestBed } from '@angular/core/testing';

import { SigningAuthorityService } from './signing-authority.service';

describe('SigningAuthorityService', () => {
  let service: SigningAuthorityService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SigningAuthorityService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
