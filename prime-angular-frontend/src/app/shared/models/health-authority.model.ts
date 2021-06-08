import { Contact } from '@lib/models/contact.model';

export interface HealthAuthority {
  name: string;
  careTypes: string[];
  vendors: string[];
  privacyOfficer: Contact;
  technicalSupports: Contact[];
  pharmanetAdministrators: Contact[];
}
