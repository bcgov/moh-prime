import { SiteStatusType } from '@lib/enums/site-status.enum';
import { HealthAuthorityVendor } from './health-authority-vendor.model';

export interface HealthAuthoritySiteAdminList {
  id?: number;
  healthAuthorityOrganizationId: number;
  healthAuthorityName: string;
  siteName;
  pec: string;
  flagged: boolean;
  healthAuthorityVendor: HealthAuthorityVendor;
  readonly submittedDate: string;
  readonly approvedDate: string;
  readonly status: SiteStatusType;
  readonly authorizedUserName: string;
  readonly authorizedUserEmail: string;
  readonly adjudicatorIdir: string;
}
