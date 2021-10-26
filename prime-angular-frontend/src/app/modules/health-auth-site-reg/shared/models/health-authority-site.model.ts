import { Contact } from '@lib/models/contact.model';
import { Address } from '@lib/models/address.model';
import { HealthAuthorityEnum } from '@lib/enums/health-authority.enum';
import { RemoteUser } from '@lib/models/remote-user.model';
import { BusinessDay } from '@lib/models/business-day.model';
import { SiteStatusType } from '@lib/enums/site-status.enum';

export interface HealthAuthoritySite {
  id?: number;
  healthAuthorityOrganizationId: HealthAuthorityEnum;
  healthAuthorityVendorId: string;
  healthAuthorityCareTypeId: string;
  siteName: string;
  siteId: string;
  securityGroupCode: number;
  physicalAddress: Address;
  businessHours: BusinessDay[];
  remoteUsers: RemoteUser[];
  healthAuthorityPharmanetAdministratorId: number;
  healthAuthorityPharmanetAdministrator: Contact;
  healthAuthorityTechnicalSupportId: number;
  healthAuthorityTechnicalSupport: Contact;
  // Indicates that a user has progressed through the entire registration, and
  // reached the overview page switching them from wizard to spoke navigation
  completed: boolean;
  submittedDate: string;
  approvedDate: string;
  status: SiteStatusType;
}
