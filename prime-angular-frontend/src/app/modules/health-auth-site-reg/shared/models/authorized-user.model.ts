import { AuthorizedUserStatusEnum } from '@health-auth/shared/enums/authorized-user-status.enum';

export interface AuthorizedUser {
  userId: string;
  status: AuthorizedUserStatusEnum;
}
