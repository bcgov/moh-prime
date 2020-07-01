import { TestBed, async, inject } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { MockAuthenticationService } from 'test/mocks/mock-authentication.service';

import { AuthorizationRedirectGuard } from './authorization-redirect.guard';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { AuthenticationService } from '../services/authentication.service';

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
          provide: AuthenticationService,
          useClass: MockAuthenticationService
        }
      ]
    });
  });

  it('should create', inject([AuthorizationRedirectGuard], (guard: AuthorizationRedirectGuard) => {
    expect(guard).toBeTruthy();
  }));
});
