import { Address } from '@shared/models/address.model';
import { RemoteUser } from '@registration/shared/models/remote-user.model';
import { BusinessDay } from '@registration/shared/models/business-day.model';
import { SiteStatusType } from '@registration/shared/enum/site-status.enum';

export interface HealthAuthoritySite {
  id?: number;
  healthAuthorityOrganizationId: number;
  vendorCode: number;
  siteName: string;
  siteId: string;
  securityGroup: string;
  healthAuthorityCareTypeId: number;
  physicalAddress: Address;
  businessHours: BusinessDay[];
  remoteUsers: RemoteUser[];
  siteAdministratorId: number;
  // Indicates that a user has progressed through the entire registration, and
  // reached the overview page switching them from wizard to spoking navigation
  completed: boolean;
  submittedDate: string;
  approvedDate: string;
  status: SiteStatusType;
}
