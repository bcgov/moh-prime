import { TestBed } from '@angular/core/testing';

import { KeycloakService } from 'keycloak-angular';

import { AuthorizationService } from './authorization.service';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';

describe('AuthorizationService', () => {
  let service: AuthorizationService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        KeycloakService
      ]
    });
    service = TestBed.inject(AuthorizationService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
