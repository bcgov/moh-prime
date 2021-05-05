import { TestBed } from '@angular/core/testing';

import { KeycloakUtilsService } from './keycloak-utils.service';

describe('KeycloakUtilsService', () => {
  let service: KeycloakUtilsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(KeycloakUtilsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
