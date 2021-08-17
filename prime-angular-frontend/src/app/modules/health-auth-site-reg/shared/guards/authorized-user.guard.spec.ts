import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { MatSnackBarModule } from '@angular/material/snack-bar';

import { KeycloakService } from 'keycloak-angular';

import { AuthorizedUserGuard } from './authorized-user.guard';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { CapitalizePipe } from '@shared/pipes/capitalize.pipe';

describe('AuthorizedUserGuard', () => {
  let guard: AuthorizedUserGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        RouterTestingModule,
        MatSnackBarModule
      ],
      providers: [
        AuthorizedUserGuard,
        KeycloakService,
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        },
        CapitalizePipe
      ]
    });
    guard = TestBed.inject(AuthorizedUserGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
