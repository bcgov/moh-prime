import { User } from '@auth/shared/models/user.model';

export interface Token {
  user: User;
}
