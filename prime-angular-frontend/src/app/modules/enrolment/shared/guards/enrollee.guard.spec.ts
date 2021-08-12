import { TestBed, waitForAsync, inject } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { MockAuthService } from 'test/mocks/mock-auth.service';

import { EnrolleeGuard } from './enrollee.guard';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { AuthService } from '@auth/shared/services/auth.service';
import { PermissionService } from '@auth/shared/services/permission.service';
import { MockPermissionService } from 'test/mocks/mock-permission.service';
import { Role } from '@auth/shared/enum/role.enum';

describe('EnrolleeGuard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          RouterTestingModule
        ],
        providers: [
          EnrolleeGuard,
          {
            provide: APP_CONFIG,
            useValue: APP_DI_CONFIG
          },
          {
            provide: AuthService,
            useClass: MockAuthService
          },
          {
            provide: PermissionService,
            useClass: MockPermissionService
          }
        ]
      }
    );
  });

  it('should create', inject([EnrolleeGuard], (guard: EnrolleeGuard) => {
    // authService.loggedIn = true;
    // authService.role = Role.ADMIN;
    expect(guard).toBeTruthy();
  }));
});
