import { Pipe, PipeTransform } from '@angular/core';

import { Role } from '@auth/shared/enum/role.enum';

import { PermissionService } from '@auth/shared/services/permission.service';

@Pipe({
  name: 'inRole'
})
export class RolePipe implements PipeTransform {
  constructor(
    private permissionService: PermissionService
  ) { }

  transform(roles: Role | Role[]): boolean {
    return this.permissionService.hasRoles(roles);
  }
}
