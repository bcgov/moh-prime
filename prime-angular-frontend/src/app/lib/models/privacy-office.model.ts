import { Address } from '@lib/models/address.model';

export interface PrivacyOffice {
  email: string;
  phone: string;
  physicalAddress: Address;
  privacyOfficer: {
    id: number;
    firstName: string;
    lastName: string;
    email: string;
    phone: string;
    phoneExtension?: string;
    smsPhone?: string;
  };
}
