import { Address } from '@lib/models/address.model';

export interface Person {
  id?: number;

  email: string;
  firstName: string;
  lastName: string;

  mailingAddressId?: number;
  mailingAddress?: Address;
  physicalAddressId?: number;
  physicalAddress: Address;
  phone: string;
  smsPhone?: string;

  fax?: string;
  jobRoleTitle: string;
}
