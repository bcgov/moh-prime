import { TestBed } from '@angular/core/testing';

import { AuthTokenService } from './auth-token.service';

describe('AuthTokenService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: AuthTokenService = TestBed.get(AuthTokenService);
    expect(service).toBeTruthy();
  });
});
