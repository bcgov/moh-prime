import { Contact } from '@lib/models/contact.model';
import { PrivacyOffice } from '@adjudication/shared/models/privacy-office.model';

export interface HealthAuthority {
  name: string;
  careTypes: string[];
  vendorCodes: number[];
  privacyOffice: PrivacyOffice;
  technicalSupports: Contact[];
  pharmanetAdministrators: Contact[];
}
