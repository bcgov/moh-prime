import { TestBed } from '@angular/core/testing';

import { MockAuthenticationService } from 'test/mocks/mock-authentication.service';

import { AuthenticationService } from './authentication.service';

describe('AuthenticationService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    providers: [
      {
        provide: AuthenticationService,
        useClass: MockAuthenticationService
      }
    ]
  }));

  it('should create', () => {
    const service: AuthenticationService = TestBed.inject(AuthenticationService);
    expect(service).toBeTruthy();
  });
});
