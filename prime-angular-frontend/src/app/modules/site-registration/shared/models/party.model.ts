import { Address } from '@shared/models/address.model';

export interface Party {
  id: number;
  userId: number;
  addressId: number;
  address: Address;
  HPDID: string;
  firstName: string;
  lastName: string;
  dateOfBirth: string;
  jobRoleTitle: string;
  email: string;
  phone: string;
  fax?: string;
  SMSPhone?: string;
}
