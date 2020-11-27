import { FacilityTypeEnum } from '@shared/enums/facility-type.enum';
import { HealthAuthorityEnum } from '@shared/enums/health-authority.enum';

export interface HealthAuthority {
  id: number;
  enrolleeId: number;
  healthAuthorityCode: HealthAuthorityEnum;
  facilityCode: FacilityTypeEnum;
}
