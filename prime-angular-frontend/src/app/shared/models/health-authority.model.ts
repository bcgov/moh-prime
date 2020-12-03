import { FacilityEnum } from '@shared/enums/facility.enum';
import { HealthAuthorityEnum } from '@shared/enums/health-authority.enum';

export interface HealthAuthority {
  id: number;
  enrolleeId: number;
  healthAuthorityCode: HealthAuthorityEnum;
  facilityCode: FacilityEnum;
}
