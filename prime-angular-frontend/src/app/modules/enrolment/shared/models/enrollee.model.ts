import { Address } from './address.model';

// TODO rename to something other than Enrollee
export interface Enrollee {
  id?: number;
  userId: string;

  // TODO duplicated in EnrolmentCertification
  firstName: string;
  middleName: string;
  lastName: string;
  preferredFirstName: string;
  preferredMiddleName: string;
  preferredLastName: string;
  dateOfBirth: string;
  licensePlate: string;

  physicalAddress: Address;
  mailingAddress?: Address;
  contactEmail: string;
  contactPhone: string;
  voicePhone: string;
  voiceExtension: string;
}
