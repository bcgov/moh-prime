import { TestBed, async, inject } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { KeycloakService } from 'keycloak-angular';

import { MockKeycloakService } from 'test/mocks/mock-keycloak.service';

import { AuthenticationGuard } from './authentication.guard';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';

describe('AuthenticationGuard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule
      ],
      providers: [
        AuthenticationGuard,
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        },
        {
          provide: KeycloakService,
          useValue: MockKeycloakService
        }
      ]
    });
  });

  it('should be injected', inject([AuthenticationGuard], (guard: AuthenticationGuard) => {
    expect(guard).toBeTruthy();
  }));
});
