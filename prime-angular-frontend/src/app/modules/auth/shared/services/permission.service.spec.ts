import { inject, TestBed } from '@angular/core/testing';
import { Role } from '../enum/role.enum';
import { AccessTokenService } from './access-token.service';

import { PermissionService } from './permission.service';

class MockAccessTokenService {
  constructor(private _roles: Role[]) {

  }
  public roles(allRoles?: boolean): string[] {
    return Object.values(this._roles);
  }
}

describe('PermissionService', () => {
  let permissionService: PermissionService;
  let accessTokenService: MockAccessTokenService;
  let spy: any;

  beforeEach(() => {
    accessTokenService = new MockAccessTokenService([Role.ADMIN]);
    TestBed.configureTestingModule({
      providers: [
        {
          provide: AccessTokenService,
          useClass: MockAccessTokenService
        }
      ],
    })
      .compileComponents();
  });

  it('should be created', inject([PermissionService], (service: PermissionService) => {
    expect(service).toBeTruthy();
  }));

  it('should work for hasRoles', inject([PermissionService], (service: PermissionService) => {
    expect(service.hasRoles(Role.ADMIN)).toBe(true);
    expect(service.hasRoles(Role.ENROLLEE)).toBe(false);
  }));

  it('should work for hasRoles with mutiple roles', inject([PermissionService], (service: PermissionService) => {
    expect(service.hasRoles(Role.ENROLLEE, Role.PHSA_IMMUNIZER)).toBe(false);
  }));

  it('should work for hasAnyRole', inject([PermissionService], (service: PermissionService) => {
    expect(service.hasAnyRole(Role.ADMIN, Role.SUPER_ADMIN)).toBe(true);
  }));
});
