import { HealthAuthorityEnum } from '@lib/enums/health-authority.enum';
import { SiteStatusType } from '@lib/enums/site-status.enum';

import { BaseHealthAuthoritySite } from '@health-auth/shared/models/base-health-authority-site.model';
import { HealthAuthoritySiteUtils } from '@health-auth/shared/classes/health-authority-site-utils.class';

export abstract class AbstractBaseHealthAuthoritySite implements BaseHealthAuthoritySite {
  protected constructor(
    public id: number,
    public healthAuthorityOrganizationId: HealthAuthorityEnum,
    public readonly completed: boolean,
    public readonly submittedDate: string,
    public readonly approvedDate: string,
    public readonly status: SiteStatusType
  ) {
    this.id = id ?? 0;
    this.healthAuthorityOrganizationId = healthAuthorityOrganizationId;
    // Indicates that a user has progressed through the entire workflow, and
    // reached the overview page switching them from wizard to spoke navigation
    this.completed = completed;
    this.submittedDate = submittedDate;
    this.approvedDate = approvedDate;
    this.status = status;
  }

  public isIncomplete(): boolean {
    return HealthAuthoritySiteUtils.isIncomplete(this);
  }

  public isInReview(): boolean {
    return HealthAuthoritySiteUtils.isInReview(this);
  }

  public isLocked(): boolean {
    return HealthAuthoritySiteUtils.isLocked(this);
  }

  public isApproved(): boolean {
    return HealthAuthoritySiteUtils.isApproved(this);
  }

  public withinRenewalPeriod(): boolean {
    return HealthAuthoritySiteUtils.withinRenewalPeriod(this);
  }

  public getExpiryDate(): string | null {
    return HealthAuthoritySiteUtils.getExpiryDate(this);
  }
}
