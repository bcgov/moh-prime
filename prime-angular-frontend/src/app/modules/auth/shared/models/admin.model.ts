import { User } from './user.model';

export interface Admin extends Omit<User, 'username'> {
  id?: number;
  userId: string; // Keycloak identifier
  // TODO map idir to username and drop Omit<...>
  idir: string;
}
