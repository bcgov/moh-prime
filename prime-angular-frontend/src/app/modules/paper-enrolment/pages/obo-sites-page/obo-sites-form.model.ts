import { OboSite } from '@enrolment/shared/models/obo-site.model';
import { CareSetting } from '@enrolment/shared/models/care-setting.model';
import { EnrolleeHealthAuthority } from '@shared/models/enrollee-health-authority.model';

export interface OboSitesFormModel {
  oboSites: OboSite[];
  enrolleeCareSettings?: CareSetting[];
  enrolleeHealthAuthorities?: EnrolleeHealthAuthority[];
}
