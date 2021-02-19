import { inject, TestBed } from '@angular/core/testing';
import { Role } from '@auth/shared/enum/role.enum';
import { PermissionService } from '@auth/shared/services/permission.service';
import { MockPermissionService } from 'test/mocks/mock-permission-service';
import { RolePipe } from './role-pipe';

describe('RolePipe', () => {
  let spy: any;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        RolePipe,
        {
          provide: PermissionService,
          useClass: MockPermissionService
        }
      ]
    });
  });

  it('should create', inject([RolePipe], (rolePipe: RolePipe) => {
    expect(rolePipe).toBeTruthy();
  }));

  it('should transform', inject([RolePipe, PermissionService],
    (rolePipe: RolePipe, permissionService: PermissionService) => {
      spy = spyOn(permissionService, 'hasRoles').and.returnValue(true);
      expect(rolePipe.transform(Role.ADMIN)).toBe(true);
      expect(rolePipe.transform(Role.ADMIN, 'in')).toBe(true);
      expect(rolePipe.transform(Role.ADMIN, 'notIn')).toBe(false);
    }));

  it('should transform from multiple roles', inject([RolePipe, PermissionService],
    (rolePipe: RolePipe, permissionService: PermissionService) => {
      spy = spyOn(permissionService, 'hasRoles').and.returnValue(true);
      expect(rolePipe.transform([Role.ADMIN, Role.SUPER_ADMIN])).toBe(true);
      expect(rolePipe.transform([Role.ADMIN, Role.SUPER_ADMIN], 'in')).toBe(true);
    }));
});
