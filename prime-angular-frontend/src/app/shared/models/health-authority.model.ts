import { Contact } from '@lib/models/contact.model';
import { PrivacyOffice } from '@lib/models/privacy-office.model';

import { HealthAuthorityVendor } from '@health-auth/shared/models/health-authority-vendor.model';
import { HealthAuthorityCareType } from '@health-auth/shared/models/health-authority-care-type.model';

export interface HealthAuthority {
  id?: number;
  name: string;
  careTypes: HealthAuthorityCareType[];
  vendors: HealthAuthorityVendor[];
  privacyOffice: PrivacyOffice;
  technicalSupports: Contact[];
  pharmanetAdministrators: Contact[];
}
