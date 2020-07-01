import { TestBed, inject } from '@angular/core/testing';

import { RegistrantGuard } from './registrant.guard';
import { RouterTestingModule } from '@angular/router/testing';
import { EnrolleeGuard } from '@enrolment/shared/guards/enrollee.guard';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { AuthenticationService } from '@auth/shared/services/authentication.service';
import { MockAuthenticationService } from 'test/mocks/mock-authentication.service';

describe('RegistrantGuard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          RouterTestingModule
        ],
        providers: [
          RegistrantGuard,
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
