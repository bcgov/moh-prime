import { HealthAuthorityEnum } from '@shared/enums/health-authority.enum';

export interface HAAuthorizedUser {
  id: number;
  firstName: string;
  lastName: string;
  dateOfBirth: string;
  email: string;
  healthAuthorityCode: HealthAuthorityEnum;
}
