import moment from 'moment';

import { DateUtils } from '@lib/utils/date-utils.class';
import { SiteStatusType } from '@lib/enums/site-status.enum';

import { BaseHealthAuthoritySite } from '@health-auth/shared/models/base-health-authority-site.model';

export class HealthAuthoritySiteUtils {
  public static isIncomplete(healthAuthoritySite: BaseHealthAuthoritySite): boolean {
    return !healthAuthoritySite.submittedDate || (
      healthAuthoritySite.submittedDate
      && !healthAuthoritySite.approvedDate
      && healthAuthoritySite.status === SiteStatusType.EDITABLE
    );
  }

  public static isInReview(healthAuthoritySite: BaseHealthAuthoritySite): boolean {
    return healthAuthoritySite.status === SiteStatusType.IN_REVIEW;
  }

  public static isLocked(healthAuthoritySite: BaseHealthAuthoritySite): boolean {
    return healthAuthoritySite.status === SiteStatusType.LOCKED;
  }

  public static isApproved(healthAuthoritySite: BaseHealthAuthoritySite): boolean {
    return healthAuthoritySite.status === SiteStatusType.EDITABLE
      && !!healthAuthoritySite.approvedDate;
  }

  public static withinRenewalPeriod(healthAuthoritySite: BaseHealthAuthoritySite): boolean {
    return DateUtils.withinRenewalPeriod(HealthAuthoritySiteUtils.getExpiryDate(healthAuthoritySite));
  }

  public static getExpiryDate(healthAuthoritySite: BaseHealthAuthoritySite): string | null {
    return (healthAuthoritySite.submittedDate)
      ? moment(healthAuthoritySite.submittedDate).add(1, 'year').format()
      : null;
  }
}
