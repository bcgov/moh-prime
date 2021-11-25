import moment from 'moment';

import { Party } from '@lib/models/party.model';
import { Contact } from '@lib/models/contact.model';
import { Address } from '@lib/models/address.model';
import { RemoteUser } from '@lib/models/remote-user.model';
import { BusinessDay } from '@lib/models/business-day.model';
import { SiteStatusType } from '@lib/enums/site-status.enum';
import { Admin } from '@auth/shared/models/admin.model';
import { Vendor } from './vendor.model';
import { BusinessLicence } from './business-licence.model';
import { IndividualDeviceProvider } from './individual-device-provider.model';

// TODO rename to CommunitySite and split out common properties to Site interface
export class Site {
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
  individualDeviceProviders: IndividualDeviceProvider[];
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
  activeBeforeRegistration: boolean;

  public static getExpiryDate(site: Site | SiteListViewModel): string | null {
    if (!site) {
      return null;
    }

    // Expiry based on business licence expiry date, unless not present
    // or deferred, which defaults to using the submitted date of the site
    return (site.businessLicence?.expiryDate)
      ? site.businessLicence?.expiryDate
      : (site.submittedDate)
        ? moment(site.submittedDate).add(1, 'year').format()
        : null;
  }
}

export interface SiteListViewModel extends Pick<Site, 'id' | 'physicalAddress' | 'doingBusinessAs' | 'submittedDate' | 'careSettingCode' | 'siteVendors' | 'completed' | 'pec' | 'status' | 'businessLicence' | 'flagged' | 'approvedDate'> {
  adjudicatorIdir: string;
  remoteUserCount: number;
  flagged: boolean;
}
