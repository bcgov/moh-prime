import { BusinessDay } from '@lib/modules/business-hours/models/business-day.model';
import { VendorConfig } from '@config/config.model';
import { Address } from '@shared/models/address.model';

import { BusinessLicenceDocument } from './business-licence-document.model';
import { Organization } from './organization.model';
import { Party } from './party.model';
import { RemoteUser } from './remote-user.model';
import { Vendor } from './vendor.model';

export interface Site {
  id?: number;
  organizationId: number;
  organization: Organization;
  // Provision is aka the Signing Authority
  provisionerId: number;
  provisioner: Party;
  // Forms -----
  organizationTypeCode: number;
  siteVendors: Vendor[];
  businessLicenceDocuments: BusinessLicenceDocument[];
  doingBusinessAs: string;
  physicalAddressId?: number;
  physicalAddress: Address;
  businessHours: BusinessDay[];
  remoteUsers: RemoteUser[];
  administratorPharmaNetId?: number;
  administratorPharmaNet: Party;
  privacyOfficerId?: number;
  privacyOfficer: Party;
  technicalSupportId?: number;
  technicalSupport: Party;
  // States -----
  completed: boolean;
  approvedDate: string;
  submittedDate: string;
  // Admin -----
  pec: string;
}
