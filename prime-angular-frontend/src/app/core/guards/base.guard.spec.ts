import { TestBed, inject } from '@angular/core/testing';

import { MockAuthService } from 'test/mocks/mock-auth.service';

import { BaseGuard } from './base.guard';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { AuthService } from '@auth/shared/services/auth.service';

describe('BaseGuard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        BaseGuard,
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        },
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
