import { TestBed } from '@angular/core/testing';

import { MockAuthService } from 'tests/mocks/mock-auth.service';

import { AuthService } from './auth.service';

describe('AuthService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    providers: [
      {
        provide: AuthService,
        useClass: MockAuthService
      }
    ]
  }));

  it('should create', () => {
    const service: AuthService = TestBed.get(AuthService);
    expect(service).toBeTruthy();
  });
});
