import { HttpClientTestingModule } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { AuthService } from '@auth/shared/services/auth.service';
import { KeycloakService } from 'keycloak-angular';
import { MockAuthService } from 'test/mocks/mock-auth.service';

import { UnderagedGuard } from './underaged.guard';

describe('UnderagedGuard', () => {
  let guard: UnderagedGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        RouterTestingModule
      ],
      providers: [
        {
          provide: AuthService,
          useClass: MockAuthService
        },
        KeycloakService
      ]
    });
    guard = TestBed.inject(UnderagedGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
