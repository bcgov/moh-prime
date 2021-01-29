import { User } from './user.model';

export interface Admin extends User {
  id?: number;
  userId: string; // Keycloak identifier
  email: string;
  firstName: string;
  lastName: string;
  // TODO consolidate `idir` into `username` on User
  // username already exists, but is unused for admins
  idir: string;
}
