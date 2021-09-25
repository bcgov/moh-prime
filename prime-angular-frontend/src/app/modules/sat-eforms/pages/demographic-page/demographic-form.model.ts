import { Address } from '@shared/models/address.model';

export interface DemographicForm {
  firstName: string;
  lastName: string;
  givenNames: string;
  dateOfBirth: string;
  preferredFirstName: string;
  preferredMiddleName: string;
  preferredLastName: string;
  verifiedAddress?: Address;
  physicalAddress?: Address;
  email: string;
  phone: string;
}
