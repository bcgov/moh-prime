import { Role } from '@auth/shared/enum/role.enum';

export interface User {
  role: Role.APPLICANT | Role.ADMIN;
}
