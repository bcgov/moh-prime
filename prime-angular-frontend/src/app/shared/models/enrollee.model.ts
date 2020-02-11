import { Address } from '../../modules/enrolment/shared/models/address.model';
import { EnrolleeProfile } from './enrollee-profile.model';

export interface Enrollee extends EnrolleeProfile {
  id?: number;
  hpdid: string;
  userId: string;
  physicalAddress: Address;
  mailingAddress?: Address;
  voicePhone: string;
  voiceExtension?: string;
  contactEmail: string;
  contactPhone?: string;
  expiryDate?: string;
}
