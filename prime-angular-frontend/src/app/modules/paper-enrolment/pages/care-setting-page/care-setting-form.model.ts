import { CareSetting } from '@enrolment/shared/models/care-setting.model';
import { EnrolleeHealthAuthority } from '@shared/models/enrollee-health-authority.model';

export interface CareSettingForm {
  careSettings: CareSetting[],
  enrolleeHealthAuthorities: EnrolleeHealthAuthority[];
}
