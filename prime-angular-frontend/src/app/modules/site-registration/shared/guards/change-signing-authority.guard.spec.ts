import { TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { MatSnackBarModule } from '@angular/material/snack-bar';

import { KeycloakService } from 'keycloak-angular';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { ChangeSigningAuthorityGuard } from './change-signing-authority.guard';

describe('ChangeSigningAuthorityGuard', () => {
  let guard: ChangeSigningAuthorityGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        HttpClientTestingModule,
        MatSnackBarModule
      ],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        },
        KeycloakService
      ]
    });
    guard = TestBed.inject(ChangeSigningAuthorityGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
