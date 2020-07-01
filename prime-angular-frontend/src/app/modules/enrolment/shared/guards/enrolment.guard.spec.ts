import { TestBed, async, inject } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { MockAuthenticationService } from 'test/mocks/mock-authentication.service';

import { EnrolmentGuard } from './enrolment.guard';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { AuthenticationService } from '@auth/shared/services/authentication.service';

describe('EnrolmentGuard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        RouterTestingModule
      ],
      providers: [
        EnrolmentGuard,
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

  it('should create', inject([EnrolmentGuard], (guard: EnrolmentGuard) => {
    expect(guard).toBeTruthy();
  }));
});
