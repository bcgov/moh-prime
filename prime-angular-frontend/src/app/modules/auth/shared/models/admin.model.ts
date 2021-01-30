import { User } from './user.model';

export interface Admin extends Omit<User, 'username'> {
  userId: string; // Keycloak identifier
  // TODO consolidate `idir` into `username` on User
  // username already exists, but is unused for admins
  idir: string;
}
