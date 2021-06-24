import { HealthAuthorityEnum } from '@shared/enums/health-authority.enum';

export interface EnrolleeHealthAuthority {
  id: number;
  enrolleeId: number;
  healthAuthorityCode: HealthAuthorityEnum;
}
