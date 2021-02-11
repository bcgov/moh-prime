import { Pipe, PipeTransform } from '@angular/core';

import { PermissionService } from '@auth/shared/services/permission.service';

@Pipe({
  name: 'inRole'
})
export class PermissionPipe implements PipeTransform {
  constructor(
    private permissionService: PermissionService
  ) { }

  transform(...roles: any): boolean {
    return this.permissionService.hasRoles(...roles);
  }
}
