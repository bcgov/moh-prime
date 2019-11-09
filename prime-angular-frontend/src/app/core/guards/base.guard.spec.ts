import { TestBed, async, inject } from '@angular/core/testing';

import { MockAuthService } from 'test/mocks/mock-auth.service';

import { BaseGuard } from './base.guard';
import { AuthService } from '@auth/shared/services/auth.service';

describe('BaseGuard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        BaseGuard,
        {
          provide: AuthService,
          useClass: MockAuthService
        }
      ]
    });
  });

  it('should create', inject([BaseGuard], (guard: BaseGuard) => {
    expect(guard).toBeTruthy();
  }));
});
