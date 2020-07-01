import { TestBed, async, inject } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { MockAuthenticationService } from 'test/mocks/mock-authentication.service';

import { EnrolleeGuard } from './enrollee.guard';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { AuthenticationService } from '@auth/shared/services/authentication.service';

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
            provide: AuthenticationService,
            useClass: MockAuthenticationService
          }
        ]
      }
    );
  });

  it('should create', inject([EnrolleeGuard], (guard: EnrolleeGuard) => {
    expect(guard).toBeTruthy();
  }));
});
