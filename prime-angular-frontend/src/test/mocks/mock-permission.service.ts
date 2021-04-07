import { Role } from '@auth/shared/enum/role.enum';
import { IPermissionService } from '@auth/shared/services/permission.service';

export class MockPermissionService implements IPermissionService {
  constructor() { }

  public hasRoles(...roles: Role[]): boolean {
    return true;
  }

  public hasAnyRole(...roles: Role[]): boolean {
    return false;
  }
}
