import { HttpClientTestingModule } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { AuthService } from '@auth/shared/services/auth.service';
import { AppRoutes } from 'app/app.routes';
import { UnderagedComponent } from '@lib/modules/root-routes/components/underaged/underaged.component';
import { KeycloakService } from 'keycloak-angular';
import { MockAuthService } from 'test/mocks/mock-auth.service';

import { UnderagedGuard } from './underaged.guard';
import { AppRoutes } from 'app/app.routes';
import { UnderagedComponent } from '@lib/modules/root-routes/components/underaged/underaged.component';

describe('UnderagedGuard', () => {
  let guard: UnderagedGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        RouterTestingModule.withRoutes([
          {
            path: AppRoutes.UNDERAGED,
            component: UnderagedComponent
          }
        ])
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
