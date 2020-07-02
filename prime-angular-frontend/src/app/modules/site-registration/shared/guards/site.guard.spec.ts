import { TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { KeycloakService } from 'keycloak-angular';

import { SiteGuard } from './site.guard';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { AuthenticationService } from '@auth/shared/services/authentication.service';
import { SiteRegistrationModule } from '@registration/site-registration.module';

describe('SiteGuard', () => {
  let guard: SiteGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        RouterTestingModule,
        SiteRegistrationModule
      ],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        },
        AuthenticationService,
        KeycloakService
      ]
    });
    guard = TestBed.inject(SiteGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
