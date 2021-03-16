import { Address } from '@shared/models/address.model';
import { Admin } from '@auth/shared/models/admin.model';

import { BusinessLicenceDocument } from './business-licence-document.model';
import { BusinessDay } from './business-day.model';
import { Party } from './party.model';
import { RemoteUser } from './remote-user.model';
import { Vendor } from './vendor.model';
import { Contact } from '../../../../lib/models/contact.model';
import { SiteStatusType } from '../enum/site-status.enum';
import { BusinessLicence } from './business-licence.model';

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
  businessLicenceDocuments: BusinessLicenceDocument[];
  businessLicenceGuid: string;
  deferredLicenceReason: string;
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
  completed: boolean;
  approvedDate: string;
  submittedDate: string;
  // Admin -----
  adjudicatorId: number;
  adjudicator: Admin;
  status: SiteStatusType;
  pec: string;
}

export interface SiteListViewModel extends
  Pick<Site, 'id' | 'physicalAddress' | 'doingBusinessAs' | 'submittedDate' | 'careSettingCode'
  | 'siteVendors' | 'completed' | 'pec' | 'status' | 'businessLicence'> {
  adjudicatorIdir: string;
  remoteUserCount: number;
}
