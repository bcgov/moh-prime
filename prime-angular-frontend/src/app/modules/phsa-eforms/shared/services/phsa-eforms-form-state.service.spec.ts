import { TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';

import { KeycloakService } from 'keycloak-angular';

import { MockAuthService } from 'test/mocks/mock-auth.service';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { AuthService } from '@auth/shared/services/auth.service';
import { PhsaEformsFormStateService } from './phsa-eforms-form-state.service';

describe('PhsaEformsFormStateService', () => {
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
        },
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        },
      ]
    });
  });

  it('should be created', () => {
    const service: PhsaEformsFormStateService = TestBed.inject(PhsaEformsFormStateService);
    expect(service).toBeTruthy();
  });
});
