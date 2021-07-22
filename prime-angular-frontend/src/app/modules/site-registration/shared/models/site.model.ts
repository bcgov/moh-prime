import { Party } from '@lib/models/party.model';
import { Contact } from '@lib/models/contact.model';
import { Address } from '@shared/models/address.model';
import { Admin } from '@auth/shared/models/admin.model';

import { Vendor } from './vendor.model';
import { RemoteUser } from './remote-user.model';
import { BusinessDay } from './business-day.model';
import { BusinessLicence } from './business-licence.model';
import { SiteStatusType } from '../enum/site-status.enum';

export interface Site {
  id?: number;
  organizationId: number;
  // Provision is aka the Signing Authority
  provisionerId: number;
  provisioner: Party;
  // Forms -----
  careSettingCode: number;
  siteVendors: Vendor[];
  businessLicence: BusinessLicence;
  doingBusinessAs: string;
  physicalAddressId?: number;
  physicalAddress: Address;
  businessHours: BusinessDay[];
  remoteUsers: RemoteUser[];
  administratorPharmaNetId?: number;
  administratorPharmaNet: Contact;
  privacyOfficerId?: number;
  privacyOfficer: Contact;
  technicalSupportId?: number;
  technicalSupport: Contact;
  // States -----
  // Indicates that a user has progressed through the entire enrolment, and
  // reached the overview page switching them from wizard to spoking navigation
  completed: boolean;
  submittedDate: string;
  approvedDate: string;
  // Admin -----
  adjudicatorId: number;
  adjudicator: Admin;
  status: SiteStatusType;
  pec: string;
  flagged: boolean;
}

export interface SiteListViewModel extends Pick<Site, 'id' | 'physicalAddress' | 'doingBusinessAs' | 'submittedDate' | 'careSettingCode' | 'siteVendors' | 'completed' | 'pec' | 'status' | 'businessLicence' | 'flagged'> {
  adjudicatorIdir: string;
  remoteUserCount: number;
  flagged: boolean;
}
