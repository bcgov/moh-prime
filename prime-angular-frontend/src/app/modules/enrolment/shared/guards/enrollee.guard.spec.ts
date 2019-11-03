import { TestBed, async, inject } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { KeycloakService } from 'keycloak-angular';

import { MockKeycloakService } from 'test/mocks/mock-keycloak.service';

import { EnrolleeGuard } from './enrollee.guard';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';

describe('EnrolleeGuard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          RouterTestingModule
        ],
        providers: [
          EnrolleeGuard,
          {
            provide: APP_CONFIG,
            useValue: APP_DI_CONFIG
          },
          {
            provide: KeycloakService,
            useValue: MockKeycloakService
          }
        ]
      }
    );
  });

  it('should be injected', inject([EnrolleeGuard], (guard: EnrolleeGuard) => {
    expect(guard).toBeTruthy();
  }));
});
