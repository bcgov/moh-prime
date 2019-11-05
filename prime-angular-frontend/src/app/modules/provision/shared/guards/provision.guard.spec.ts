import { TestBed, async, inject } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { KeycloakService } from 'keycloak-angular';

import { MockKeycloakService } from 'test/mocks/mock-keycloak.service';

import { ProvisionGuard } from './provision.guard';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';

describe('ProvisionGuard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule
      ],
      providers: [
        ProvisionGuard,
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        },
        {
          provide: KeycloakService,
          useClass: MockKeycloakService
        }
      ]
    });
  });

  it('should be injected', inject([ProvisionGuard], (guard: ProvisionGuard) => {
    expect(guard).toBeTruthy();
  }));
});
