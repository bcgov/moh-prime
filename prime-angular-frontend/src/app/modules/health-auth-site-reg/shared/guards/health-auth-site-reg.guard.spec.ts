import { TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { KeycloakService } from 'keycloak-angular';

import { HealthAuthSiteRegGuard } from './health-auth-site-reg.guard';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';

describe('HealthAuthSiteRegGuard', () => {
  let guard: HealthAuthSiteRegGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        RouterTestingModule,
        MatSnackBarModule
      ],
      providers: [
        HealthAuthSiteRegGuard,
        KeycloakService,
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        }
      ]
    });
    guard = TestBed.inject(HealthAuthSiteRegGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
