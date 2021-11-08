import { SiteStatusType } from '@lib/enums/site-status.enum';
import { HealthAuthorityVendor } from './health-authority-vendor.model';

export interface HealthAuthoritySiteListItem {
  id?: number;
  healthAuthorityOrganizationId: number;
  siteName;
  pec: string;
  healthAuthorityVendor: HealthAuthorityVendor;
  readonly submittedDate: string;
  readonly approvedDate: string;
  readonly status: SiteStatusType;
  readonly authorizedUserName: string;
}
