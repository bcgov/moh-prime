import { TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { KeycloakService } from 'keycloak-angular';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { CapitalizePipe } from '@shared/pipes/capitalize.pipe';
import { HealthAuthoritySiteGuard } from './health-authority-site-guard.service';

describe('HealthAuthoritySiteGuard', () => {
  let guard: HealthAuthoritySiteGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        RouterTestingModule,
        MatSnackBarModule
      ],
      providers: [
        HealthAuthoritySiteGuard,
        KeycloakService,
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        },
        CapitalizePipe
      ]
    });
    guard = TestBed.inject(HealthAuthoritySiteGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
