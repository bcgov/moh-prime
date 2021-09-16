import { Injectable, Pipe, PipeTransform } from '@angular/core';

import { Role } from '@auth/shared/enum/role.enum';

import { PermissionService } from '@auth/shared/services/permission.service';

@Injectable({
  providedIn: 'root'
})
@Pipe({
  name: 'role'
})
export class RolePipe implements PipeTransform {
  constructor(
    private permissionService: PermissionService
  ) { }

  public transform(roles: Role | Role[], mode: 'in' | 'notIn' = 'in'): boolean {
    // For simplicity, notIn is now just implemented as a straight negate of 'in'
    // 'notIn' will yield an equivalent result of not having any role from the
    // input roles.
    return (mode === 'in') && this.permissionService.hasRoles(roles);
  }
}
