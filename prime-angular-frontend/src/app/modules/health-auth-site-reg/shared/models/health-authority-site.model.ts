import { Contact } from '@lib/models/contact.model';
import { Address } from '@shared/models/address.model';
// TODO move these into /lib
import { RemoteUser } from '@registration/shared/models/remote-user.model';
import { BusinessDay } from '@registration/shared/models/business-day.model';
import { SiteStatusType } from '@registration/shared/enum/site-status.enum';

export interface HealthAuthoritySite {
  id?: number;
  healthAuthorityOrganizationId: number;
  // healthAuthorityVendorId: number;
  // healthAuthorityVendor: Vendor;
  vendorCode: number;
  siteName: string;
  siteId: string;
  securityGroup: number;
  // healthAuthorityCareTypeId: number;
  // healthAuthorityCareType: string;
  careType: string;
  physicalAddress: Address;
  businessHours: BusinessDay[];
  remoteUsers: RemoteUser[];
  healthAuthorityPharmanetAdministratorId: number;
  healthAuthorityPharmanetAdministrator: Contact;
  healthAuthorityTechnicalSupportId: number;
  healthAuthorityTechnicalSupport: Contact;
  // Indicates that a user has progressed through the entire registration, and
  // reached the overview page switching them from wizard to spoking navigation
  completed: boolean;
  submittedDate: string;
  approvedDate: string;
  status: SiteStatusType;
}
