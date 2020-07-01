import { TestBed, async, inject } from '@angular/core/testing';

import { RegistrationGuard } from './registration.guard';
import { RouterTestingModule } from '@angular/router/testing';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { AuthenticationService } from '@auth/shared/services/authentication.service';
import { MockAuthenticationService } from 'test/mocks/mock-authentication.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { SharedModule } from '@shared/shared.module';

describe('RegistrationGuard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        HttpClientTestingModule,
        SharedModule
      ],
      providers: [
        RegistrationGuard,
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

  it('should ...', inject([RegistrationGuard], (guard: RegistrationGuard) => {
    expect(guard).toBeTruthy();
  }));
});
