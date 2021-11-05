import { HealthAuthorityEnum } from '@lib/enums/health-authority.enum';

export interface EnrolleeHealthAuthority {
  id: number;
  enrolleeId: number;
  healthAuthorityCode: HealthAuthorityEnum;
}
