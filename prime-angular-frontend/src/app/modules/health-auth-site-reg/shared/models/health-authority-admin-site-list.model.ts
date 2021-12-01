import { HealthAuthorityEnum } from '@lib/enums/health-authority.enum';
import { SiteStatusType } from '@lib/enums/site-status.enum';
import { AbstractBaseHealthAuthoritySite } from './abstract-base-health-authority-site.class';
import { BaseHealthAuthoritySite } from './base-health-authority-site.model';
import { HealthAuthorityVendor } from './health-authority-vendor.model';

export interface HealthAuthorityAdminSiteListDto extends BaseHealthAuthoritySite {
  healthAuthorityVendor: HealthAuthorityVendor;
  siteName: string;
  pec: string;
  healthAuthorityName: string;
  flagged: boolean;
  readonly authorizedUserName: string;
  readonly authorizedUserEmail: string;
  readonly adjudicatorIdir: string;
}

export class HealthAuthoritySiteAdminList extends AbstractBaseHealthAuthoritySite implements HealthAuthorityAdminSiteListDto {
  constructor(
    public id: number,
    public healthAuthorityOrganizationId: HealthAuthorityEnum,
    public healthAuthorityVendor: HealthAuthorityVendor,
    public siteName,
    public pec: string,
    public readonly completed: boolean,
    public readonly submittedDate: string,
    public readonly approvedDate: string,
    public readonly status: SiteStatusType,
    public healthAuthorityName: string,
    public flagged: boolean,
    public readonly authorizedUserName: string,
    public readonly authorizedUserEmail: string,
    public readonly adjudicatorIdir: string
  ) {
    super(id, healthAuthorityOrganizationId, completed, submittedDate, approvedDate, status);

    this.healthAuthorityVendor = healthAuthorityVendor;
    this.siteName = siteName;
    this.pec = pec;

    this.healthAuthorityName = healthAuthorityName;
    this.flagged = flagged;
    this.authorizedUserName = authorizedUserName;
    this.authorizedUserEmail = authorizedUserEmail;
    this.adjudicatorIdir = adjudicatorIdir;
  }
}
