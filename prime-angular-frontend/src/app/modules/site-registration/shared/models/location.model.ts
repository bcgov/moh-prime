import { Address } from '@shared/models/address.model';

import { Party } from './party.model';
import { Organization } from './organization.model';

export interface Location {
  id?: number;
  administratorPharmaNetId?: number;
  administratorPharmaNet: Party;
  privacyOfficerId?: number;
  privacyOfficer: Party;
  technicalSupportId?: number;
  technicalSupport: Party;
  organizationId?: number;
  organization: Organization;
  physicalAddressId?: number;
  physicalAddress: Address;
  hoursWeekend: boolean;
  hours24: boolean;
  hoursSpecial: boolean;
  doingBusinessAs?: string;
}
