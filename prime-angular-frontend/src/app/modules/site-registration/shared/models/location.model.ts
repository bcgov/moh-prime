import { BusinessDay } from '@lib/modules/business-hours/models/business-day.model';
import { Address } from '@shared/models/address.model';

import { Party } from './party.model';
import { Organization } from './organization.model';
import { Site } from './site.model';

export interface Location {
  id?: number;
  organizationId?: number;
  organization: Organization;
  name: string;
  // TODO do they need doingBusinessAs when they have location name?
  // doingBusinessAs?: string;
  physicalAddressId?: number;
  physicalAddress: Address;
  administratorPharmaNetId?: number;
  administratorPharmaNet: Party;
  privacyOfficerId?: number;
  privacyOfficer: Party;
  technicalSupportId?: number;
  technicalSupport: Party;
  businessHours: BusinessDay[];
  sites: Site[];
}
