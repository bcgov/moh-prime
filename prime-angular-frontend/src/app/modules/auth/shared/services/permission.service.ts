import { Injectable } from '@angular/core';

import { Role } from '@auth/shared/enum/role.enum';

import { AccessTokenService } from '@auth/shared/services/access-token.service';

export interface IPermissionService {
  hasRoles(...roles: Role[]): boolean;
  hasAnyRole(...roles: Role[]): boolean;
}

@Injectable({
  providedIn: 'root'
})
export class PermissionService implements IPermissionService {

  constructor(
    private accessTokenService: AccessTokenService
  ) { }

  public hasRoles(...roles: Role[]): boolean {
    const assignedRoles = this.accessTokenService.roles();
    return roles.every(r => assignedRoles.includes(r));
  }

  public hasAnyRole(...roles: Role[]): boolean {
    const assignedRoles = this.accessTokenService.roles();
    return roles.some(r => assignedRoles.includes(r));
  }
}
