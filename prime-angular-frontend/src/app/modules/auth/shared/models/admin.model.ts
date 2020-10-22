import { User } from './user.model';

export interface Admin extends User {
  id?: number;
  userId: string;
  firstName: string;
  lastName: string;
  email: string;
  // TODO consolidate `idir` into `username` on User
  // username already exists, but is unused for admins
  idir: string;
}
