import { Pipe, PipeTransform } from '@angular/core';

import { Role } from '@auth/shared/enum/role.enum';
import { RolePipe } from './role-pipe';

@Pipe({
  name: 'inRole'
})
export class InRolePipe implements PipeTransform {
  constructor(
    private rolePipe: RolePipe
  ) { }

  public transform(roles: Role | Role[]): boolean {
    return this.rolePipe.transform(roles, 'in');
  }
}
