import { Address } from '@shared/models/address.model';

export interface Person {
  id?: number;
  firstName: string;
  lastName: string;
  jobRoleTitle: string;
  email: string;
  phone: string;
  fax?: string;
  smsPhone?: string;
  physicalAddressId?: number;
  physicalAddress: Address;
  mailingAddressId?: number;
  mailingAddress?: Address;
}
