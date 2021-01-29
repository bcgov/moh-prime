import { User } from './user.model';

export interface BceidUser extends User {
  email?: string;
  firstName: string;
  lastName?: string;
}
