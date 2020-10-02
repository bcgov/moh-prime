import { TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';

import { KeycloakService } from 'keycloak-angular';

import { MockAuthService } from 'test/mocks/mock-auth.service';

import { EnrolmentFormStateService } from './enrolment-form-state.service';
import { AuthService } from '@auth/shared/services/auth.service';

describe('EnrolmentFormStateService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [
      ReactiveFormsModule,
      RouterTestingModule
    ],
    providers: [
      KeycloakService,
      {
        provide: AuthService,
        useClass: MockAuthService
      }
    ]
  }));

  it('should create', () => {
    const service: EnrolmentFormStateService = TestBed.inject(EnrolmentFormStateService);
    expect(service).toBeTruthy();
  });
});
