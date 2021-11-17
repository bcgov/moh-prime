import { Party } from '@lib/models/party.model';
import { HealthAuthorityEnum } from '@lib/enums/health-authority.enum';
import { AccessStatusEnum } from '@health-auth/shared/enums/access-status.enum';

export interface AuthorizedUser extends Party {
  employmentIdentifier: string;
  healthAuthorityCode: HealthAuthorityEnum;
  status: AccessStatusEnum;
}
