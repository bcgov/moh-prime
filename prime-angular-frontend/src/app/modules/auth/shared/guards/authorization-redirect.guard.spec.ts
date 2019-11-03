import { TestBed, async, inject } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { KeycloakService } from 'keycloak-angular';

import { MockKeycloakService } from 'test/mocks/mock-keycloak.service';

import { AuthorizationRedirectGuard } from './authorization-redirect.guard';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';

describe('AuthorizationRedirectGuard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule
      ],
      providers: [
        AuthorizationRedirectGuard,
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

  it('should be injected', inject([AuthorizationRedirectGuard], (guard: AuthorizationRedirectGuard) => {
    expect(guard).toBeTruthy();
  }));
});
