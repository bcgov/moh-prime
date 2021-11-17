import { TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { KeycloakService } from 'keycloak-angular';

import { MockAuthService } from 'test/mocks/mock-auth.service';
import { MockConfigService } from 'test/mocks/mock-config.service';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { ConfigService } from '@config/config.service';
import { AuthService } from '@auth/shared/services/auth.service';
import { EnrolmentFormStateService } from './enrolment-form-state.service';

describe('EnrolmentFormStateService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [
      ReactiveFormsModule,
      RouterTestingModule,
      HttpClientTestingModule,
      MatSnackBarModule
    ],
    providers: [
      KeycloakService,
      {
        provide: AuthService,
        useClass: MockAuthService
      },
      {
        provide: APP_CONFIG,
        useValue: APP_DI_CONFIG
      },
      {
        provide: ConfigService,
        useClass: MockConfigService
      }
    ]
  }));

  it('should create', () => {
    const service: EnrolmentFormStateService = TestBed.inject(EnrolmentFormStateService);
    expect(service).toBeTruthy();
  });
});
