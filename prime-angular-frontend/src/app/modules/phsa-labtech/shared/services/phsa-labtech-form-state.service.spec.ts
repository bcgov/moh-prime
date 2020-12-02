import { TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';

import { KeycloakService } from 'keycloak-angular';

import { MockAuthService } from 'test/mocks/mock-auth.service';

import { AuthService } from '@auth/shared/services/auth.service';

import { PhsaLabtechFormStateService } from './phsa-labtech-form-state.service';

describe('PhsaLabtechFormStateService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
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
    });
  });

  it('should be created', () => {
    const service: PhsaLabtechFormStateService = TestBed.inject(PhsaLabtechFormStateService);
    expect(service).toBeTruthy();
  });
});
