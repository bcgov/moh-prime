import { SiteStatusType } from "@lib/enums/site-status.enum";

export interface HealthAuthoritySiteList {
	id: number;
	healthAuthorityOrganizationId: number;
	siteName: string;
	submittedDate: string;
	adjudicatorIdir: string;
	status: SiteStatusType;
	pec: string;
	hasRemoteUsers: boolean;
}
