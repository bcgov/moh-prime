import { Address } from './address.model';
import { EnrolleeProfile } from './enrollee-profile.model';

export interface Enrollee extends EnrolleeProfile {
  id?: number;
  hpdid: string;
  userId: string;
  physicalAddress: Address;
  mailingAddress?: Address;
  phone: string;
  phoneExtension?: string;
  email: string;
  smsPhone?: string;
}
