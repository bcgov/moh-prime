import { AdminStatusType } from '@adjudication/shared/models/admin-status.enum';
import { User } from './user.model';

export interface Admin extends User {
  id?: number;
  userId: string; // Keycloak identifier
  idir: string;
  username: string; // e.g. jsmith@idir
  status: AdminStatusType;
}

export interface AdminUser extends Admin {
  sitesAssigned: number;
  enrolleesAssigned: number;
}
