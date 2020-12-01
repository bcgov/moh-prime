import { TestBed, waitForAsync, inject } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { MockAuthService } from 'test/mocks/mock-auth.service';

import { AuthorizationRedirectGuard } from './authorization-redirect.guard';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { AuthService } from '../services/auth.service';

describe('AuthorizationRedirectGuard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule
      ],
      providers: [
        AuthorizationRedirectGuard,
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

  it('should create', inject([AuthorizationRedirectGuard], (guard: AuthorizationRedirectGuard) => {
    expect(guard).toBeTruthy();
  }));
});
