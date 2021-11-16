import { HealthAuthorityEnum } from '@lib/enums/health-authority.enum';
import { SiteStatusType } from '@lib/enums/site-status.enum';

/**
 * @description
 * Base health authority site model containing the minimum
 * number of properties to determine the site, relationships,
 * and state.
 */
export interface BaseHealthAuthoritySite {
  id?: number;
  healthAuthorityOrganizationId: HealthAuthorityEnum;
  readonly completed: boolean;
  readonly submittedDate: string;
  readonly approvedDate: string;
  readonly status: SiteStatusType;
}
