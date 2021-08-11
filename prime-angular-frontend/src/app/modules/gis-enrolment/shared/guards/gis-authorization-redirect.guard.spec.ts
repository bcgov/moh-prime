import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { MatSnackBarModule } from '@angular/material/snack-bar';

import { KeycloakService } from 'keycloak-angular';

import { GisAuthorizationRedirectGuard } from './gis-authorization-redirect.guard';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';

xdescribe('GisAuthorizationRedirectGuard', () => {
  let guard: GisAuthorizationRedirectGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        RouterTestingModule,
        MatSnackBarModule
      ],
      providers: [
        GisAuthorizationRedirectGuard,
        KeycloakService,
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        }
      ]
    });
    guard = TestBed.inject(GisAuthorizationRedirectGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
