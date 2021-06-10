import { Contact } from '@lib/models/contact.model';

export interface HealthAuthority {
  name: string;
  careTypes: string[];
  vendorCodes: string[];
  privacyOfficers: Contact[];
  technicalSupports: Contact[];
  pharmanetAdministrators: Contact[];
}
