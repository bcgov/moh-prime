import { Contact } from '@lib/models/contact.model';

export interface AdministratorForm {
  pharmanetAdministratorId: number;
  // Navigational property not for patching into form
  pharmanetAdministrator: Contact;
}
