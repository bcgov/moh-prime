import { Role } from '../enum/role.enum';

export interface User {
  id: string;
  role: Role.APPLICANT | Role.ADMIN;
}
