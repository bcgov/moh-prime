import { inject, TestBed } from '@angular/core/testing';
import { KeycloakService } from 'keycloak-angular';
import { MockAccessTokenService } from 'test/mocks/mock-access-token.service';

import { Role } from '../enum/role.enum';
import { AccessTokenService } from './access-token.service';
import { PermissionService } from './permission.service';

describe('PermissionService', () => {
  let spy: any;

  beforeEach(() => TestBed.configureTestingModule({
    providers: [
      {
        provide: AccessTokenService,
        useClass: MockAccessTokenService
      },
      KeycloakService
    ]
  }));

  it('should be created', inject([PermissionService], (service: PermissionService) => {
    expect(service).toBeTruthy();
  }));

  it('should work for hasRoles', inject([PermissionService, AccessTokenService],
    (permissionService: PermissionService, accessTokenService: AccessTokenService) => {
      spy = spyOn(accessTokenService, 'roles').and.returnValue([Role.ADMIN, Role.SUPER_ADMIN]);
      expect(permissionService.hasRoles(Role.ADMIN)).toBe(true);
      expect(permissionService.hasRoles(Role.ADMIN, Role.SUPER_ADMIN)).toBe(true);
      expect(permissionService.hasRoles(Role.ENROLLEE)).toBe(false);
      expect(permissionService.hasRoles(Role.ENROLLEE, Role.PHSA_IMMUNIZER)).toBe(false);
    }));

  it('should work for hasAnyRole', inject([PermissionService, AccessTokenService],
    (permissionService: PermissionService, accessTokenService: AccessTokenService) => {
      spy = spyOn(accessTokenService, 'roles').and.returnValue([Role.ADMIN, Role.SUPER_ADMIN, Role.ENROLLEE]);
      expect(permissionService.hasAnyRole(Role.ADMIN, Role.SUPER_ADMIN)).toBe(true);
      expect(permissionService.hasAnyRole(Role.MANAGE_ENROLLEE)).toBe(false);
    }));
});
