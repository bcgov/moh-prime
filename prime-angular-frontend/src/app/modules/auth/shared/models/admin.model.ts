import { User } from './user.model';

export interface Admin extends User {
  id?: number;
  userId: string; // Keycloak identifier
  idir: string;
}
