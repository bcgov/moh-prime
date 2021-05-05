import { Party } from '@lib/models/party.model';
import { AccessStatusEnum } from '@health-auth/shared/enums/access-status.enum';

export interface AuthorizedUser extends Party {
  employmentIdentifier: string;
  healthAuthorityCode: number;
  status: AccessStatusEnum;
}
