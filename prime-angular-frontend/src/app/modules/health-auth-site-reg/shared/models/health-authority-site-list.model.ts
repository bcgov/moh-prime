import { HealthAuthorityEnum } from '@lib/enums/health-authority.enum';
import { HealthAuthorityVendor } from '@health-auth/shared/models/health-authority-vendor.model';
import { SiteStatusType } from '@lib/enums/site-status.enum';

import { AbstractBaseHealthAuthoritySite } from '@health-auth/shared/models/abstract-base-health-authority-site.class';
import { BaseHealthAuthoritySite } from '@health-auth/shared/models/base-health-authority-site.model';

export interface HealthAuthoritySiteListDto extends BaseHealthAuthoritySite {
  healthAuthorityVendor: HealthAuthorityVendor;
  siteName: string;
  pec: string;
}

export class HealthAuthoritySiteList extends AbstractBaseHealthAuthoritySite implements HealthAuthoritySiteListDto {
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
  ) {
    super(id, healthAuthorityOrganizationId, completed, submittedDate, approvedDate, status);

    this.healthAuthorityVendor = healthAuthorityVendor;
    this.siteName = siteName;
    this.pec = pec;
  }

  /**
   * @description
   * Convert structurally typed HealthAuthoritySiteListDto to an
   * instance of HealthAuthoritySiteList.
   */
  public static toHealthAuthoritySiteList(
    healthAuthoritySiteListDtos: HealthAuthoritySiteListDto | HealthAuthoritySiteListDto[]
  ): HealthAuthoritySiteList | HealthAuthoritySiteListDto[] | null {
    if (!healthAuthoritySiteListDtos) {
      return null;
    }

    const instantiate = (healthAuthoritySiteListDto: HealthAuthoritySiteListDto) => new HealthAuthoritySiteList(
      healthAuthoritySiteListDto.id,
      healthAuthoritySiteListDto.healthAuthorityOrganizationId,
      healthAuthoritySiteListDto.healthAuthorityVendor,
      healthAuthoritySiteListDto.siteName,
      healthAuthoritySiteListDto.pec,
      healthAuthoritySiteListDto.completed,
      healthAuthoritySiteListDto.submittedDate,
      healthAuthoritySiteListDto.approvedDate,
      healthAuthoritySiteListDto.status,
    );

    return (Array.isArray(healthAuthoritySiteListDtos))
      ? healthAuthoritySiteListDtos.map(instantiate)
      : instantiate(healthAuthoritySiteListDtos);
  }
}
