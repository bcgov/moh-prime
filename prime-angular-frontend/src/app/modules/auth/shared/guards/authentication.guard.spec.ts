import { TestBed, async, inject } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { MockAuthService } from 'test/mocks/mock-auth.service';

import { AuthenticationGuard } from './authentication.guard';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { AuthService } from '../services/auth.service';
import { Role } from '../enum/role.enum';

describe('AuthenticationGuard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule
      ],
      providers: [
        AuthenticationGuard,
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

  it('should be injected', inject([AuthenticationGuard, AuthService], (guard: AuthenticationGuard, authService: MockAuthService) => {
    authService.loggedIn = true;
    authService.role = Role.ADMIN;
    expect(guard).toBeTruthy();
  }));
});
