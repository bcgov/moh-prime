import { inject, TestBed } from '@angular/core/testing';
import { Role } from '@auth/shared/enum/role.enum';
import { PermissionService } from '@auth/shared/services/permission.service';
import { MockPermissionService } from 'test/mocks/mock-permission-service';
import { RolePipe } from '@shared/pipes/role-pipe';
import { InRolePipe } from './in-role-pipe';

describe('InRolePipe', () => {
  let spy: any;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        RolePipe,
        InRolePipe,
        {
          provide: PermissionService,
          useClass: MockPermissionService
        }
      ]
    });
  });

  it('should create', inject([InRolePipe],
    (inRolePipe: InRolePipe) => {
      expect(inRolePipe).toBeTruthy();
    }));

  it('should transform', inject([PermissionService, InRolePipe],
    (permissionService: PermissionService, inRolePipe: InRolePipe) => {
      spy = spyOn(permissionService, 'hasRoles').and.returnValue(true);
      expect(inRolePipe.transform(Role.ADMIN)).toBe(true);
    }));

  it('should transform from multiple roles', inject([PermissionService, InRolePipe],
    (permissionService: PermissionService, inRolePipe: InRolePipe) => {
      spy = spyOn(permissionService, 'hasRoles').and.returnValue(true);
      expect(inRolePipe.transform([Role.ADMIN, Role.SUPER_ADMIN])).toBe(true);
    }));
});
