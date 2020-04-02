import { Address } from './address.model';

export interface Registrant {
  id?: number;
  hpdid: string;
  userId: string;
  physicalAddress: Address;
  mailingAddress?: Address;
  voicePhone: string;
  voiceExtension?: string;
  contactEmail: string;
  contactPhone?: string;
  firstName: string;
  middleName: string;
  lastName: string;
  preferredFirstName: string;
  preferredMiddleName: string;
  preferredLastName: string;
  dateOfBirth: string;
  gpid: string;
}
