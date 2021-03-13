import { TestBed, waitForAsync, inject } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { KeycloakService } from 'keycloak-angular';

import { MockAuthService } from 'test/mocks/mock-auth.service';

import { EnrolmentGuard } from './enrolment.guard';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { AuthService } from '@auth/shared/services/auth.service';
import { ReactiveFormsModule } from '@angular/forms';
import { ConfigService } from '@config/config.service';
import { MockConfigService } from 'test/mocks/mock-config.service';

describe('EnrolmentGuard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        RouterTestingModule,
        MatSnackBarModule,
        ReactiveFormsModule
      ],
      providers: [
        EnrolmentGuard,
        KeycloakService,
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        },
        {
          provide: AuthService,
          useClass: MockAuthService
        },
        {
          provide: ConfigService,
          useClass: MockConfigService
        }
      ]
    });
  });

  it('should create', inject([EnrolmentGuard], (guard: EnrolmentGuard) => {
    expect(guard).toBeTruthy();
  }));
});
