import { Contact } from '@lib/models/contact.model';
import { PrivacyOffice } from '@adjudication/shared/models/privacy-office.model';

export interface HealthAuthority {
  id?: number;
  name: string;
  careTypes: string[];
  vendorCodes: number[];
  privacyOffice: PrivacyOffice;
  technicalSupports: Contact[];
  pharmanetAdministrators: Contact[];
}
