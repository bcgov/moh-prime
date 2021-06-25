import { Address } from '@shared/models/address.model';

export interface PrivacyOffice {
  email: string;
  phone: string;
  physicalAddress: Address;
  privacyOfficer: {
    firstName: string;
    lastName: string;
    email: string;
    phone: string;
    smsPhone?: string;
  };
}
