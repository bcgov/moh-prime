import { TestBed, async, inject } from '@angular/core/testing';

import { MockAuthenticationService } from 'test/mocks/mock-authentication.service';

import { BaseGuard } from './base.guard';
import { AuthenticationService } from '@auth/shared/services/authentication.service';

describe('BaseGuard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        BaseGuard,
        {
          provide: AuthenticationService,
          useClass: MockAuthenticationService
        }
      ]
    });
  });

  it('should create', inject([BaseGuard], (guard: BaseGuard) => {
    expect(guard).toBeTruthy();
  }));
});
