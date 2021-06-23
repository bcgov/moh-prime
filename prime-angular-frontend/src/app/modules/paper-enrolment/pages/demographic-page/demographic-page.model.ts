import { Address } from '@shared/models/address.model';

export interface Demographic {
  firstName: string;
  middleName: string;
  lastName: string;
  dateOfBirth: string;
  physicalAddress: Address;
  email: string;
  phone: string;
  phoneExtension: string;
  smsPhone: string;
}
