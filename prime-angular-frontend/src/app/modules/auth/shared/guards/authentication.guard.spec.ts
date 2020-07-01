import { TestBed, async, inject } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { MockAuthenticationService } from 'test/mocks/mock-authentication.service';

import { AuthenticationGuard } from './authentication.guard';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { AuthenticationService } from '@auth/shared/services/authentication.service';
import { Role } from '@auth/shared/enum/role.enum';

describe('AuthenticationGuard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        RouterTestingModule
      ],
      providers: [
        AuthenticationGuard,
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        },
        {
          provide: AuthenticationService,
          useClass: MockAuthenticationService
        }
      ]
    });
  });

  it('should create', inject([AuthenticationGuard, AuthenticationService], (guard: AuthenticationGuard, authenticationService: MockAuthenticationService) => {
    authenticationService.loggedIn = true;
    authenticationService.role = Role.ADMIN;
    expect(guard).toBeTruthy();
  }));
});
