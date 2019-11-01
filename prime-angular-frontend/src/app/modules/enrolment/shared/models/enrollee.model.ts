import { Address } from './address.model';

export class Enrollee {
  id?: number;
  userId: string;
  firstName: string;
  middleName: string;
  lastName: string;
  preferredFirstName: string;
  preferredMiddleName: string;
  preferredLastName: string;
  dateOfBirth: string;
  physicalAddress: Address;
  mailingAddress?: Address;
  contactEmail: string;
  contactPhone: string;
  voicePhone: string;
  voiceExtension: string;
  licensePlate: string;
}
