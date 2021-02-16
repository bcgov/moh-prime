import { inject, TestBed } from '@angular/core/testing';
import { Role } from '@auth/shared/enum/role.enum';
import { PermissionService } from '@auth/shared/services/permission.service';
import { MockPermissionService } from 'test/mocks/mock-permission-service';
import { PermissionPipe } from './permission-pipe';

describe('PermissionPipe', () => {
  let spy: any;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        PermissionPipe,
        {
          provide: PermissionService,
          useClass: MockPermissionService
        }
      ]
    });
  });

  it('should create', inject([PermissionPipe], (pipe: PermissionPipe) => {
    expect(pipe).toBeTruthy();
  }));

  it('should transform', inject([PermissionPipe, PermissionService],
    (pipe: PermissionPipe, permissionService: PermissionService) => {
      spy = spyOn(permissionService, 'hasRoles').and.returnValue(true);
      expect(pipe.transform(Role.ADMIN)).toBe(true);
    }));

  it('should transform from multiple roles', inject([PermissionPipe, PermissionService],
    (pipe: PermissionPipe, permissionService: PermissionService) => {
      spy = spyOn(permissionService, 'hasRoles').and.returnValue(true);
      expect(pipe.transform([Role.ADMIN, Role.SUPER_ADMIN])).toBe(true);
    }));

});
