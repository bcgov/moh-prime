import { Address } from '../../shared/models/address.model';

export interface Profile {
  firstName: string;
  middleName?: string;
  lastName: string;
  dateOfBirth: string;
  preferredFirstName?: string;
  preferredMiddleName?: string;
  preferredLastName?: string;
  physicalAddress: Address;
  mailingAddress?: Address;
}
