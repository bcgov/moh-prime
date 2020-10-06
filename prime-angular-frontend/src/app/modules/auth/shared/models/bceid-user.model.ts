import { User } from './user.model';

export interface BceidUser extends User {
  firstName: string;
  lastName?: string;
  email?: string;
}
