import { Address } from '@shared/models/address.model';

export interface Person {
  id?: number;

  email: string;
  firstName: string;
  lastName: string;
  physicalAddressId?: number;
  physicalAddress: Address;

  mailingAddressId?: number;
  mailingAddress?: Address;
  phone: string;
  smsPhone?: string;

  fax?: string;
  jobRoleTitle: string;
}
