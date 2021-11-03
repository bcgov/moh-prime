import moment from 'moment';

import { Address } from '@lib/models/address.model';
import { DateUtils } from '@lib/utils/date-utils.class';
import { RemoteUser } from '@lib/models/remote-user.model';
import { BusinessDay } from '@lib/models/business-day.model';
import { SiteStatusType } from '@lib/enums/site-status.enum';
import { HealthAuthorityEnum } from '@lib/enums/health-authority.enum';

import { HealthAuthorityVendor } from '@health-auth/shared/models/health-authority-vendor.model';
import { HealthAuthorityCareType } from '@health-auth/shared/models/health-authority-care-type.model';
import { HealthAuthoritySiteUpdate } from '@health-auth/shared/models/health-authority-site-update.model';
import { HealthAuthoritySiteCreate } from '@health-auth/shared/models/health-authority-site-create.model';

// TODO split up Site, CommunitySite, and HealthAuthoritySite into separate interfaces/classes
export interface HealthAuthoritySiteDto {
  id?: number;
  healthAuthorityOrganizationId: HealthAuthorityEnum;
  healthAuthorityVendor: HealthAuthorityVendor;
  healthAuthorityCareType: HealthAuthorityCareType;
  siteName;
  pec: string;
  securityGroupCode: number;
  physicalAddress: Address;
  businessHours: BusinessDay[];
  remoteUsers: RemoteUser[];
  healthAuthorityPharmanetAdministratorId: number;
  healthAuthorityTechnicalSupportId: number;
  readonly completed: boolean;
  readonly submittedDate: string;
  readonly approvedDate: string;
  readonly status: SiteStatusType;
}

export class HealthAuthoritySite implements HealthAuthoritySiteDto {
  constructor(
    public healthAuthorityOrganizationId: HealthAuthorityEnum,
    public healthAuthorityVendor: HealthAuthorityVendor,
    public healthAuthorityCareType: HealthAuthorityCareType,
    public siteName,
    public pec: string,
    public securityGroupCode: number,
    public physicalAddress: Address,
    public businessHours: BusinessDay[],
    public remoteUsers: RemoteUser[],
    public healthAuthorityPharmanetAdministratorId: number,
    public healthAuthorityTechnicalSupportId: number,
    public readonly completed: boolean,
    public readonly submittedDate: string,
    public readonly approvedDate: string,
    public readonly status: SiteStatusType,
    public id?: number
  ) {
    this.healthAuthorityOrganizationId = healthAuthorityOrganizationId;
    this.healthAuthorityVendor = healthAuthorityVendor;
    this.healthAuthorityCareType = healthAuthorityCareType;
    this.siteName = siteName;
    this.pec = pec;
    this.securityGroupCode = securityGroupCode;
    this.physicalAddress = physicalAddress;
    this.businessHours = businessHours;
    this.remoteUsers = remoteUsers;
    this.healthAuthorityPharmanetAdministratorId = healthAuthorityPharmanetAdministratorId;
    this.healthAuthorityTechnicalSupportId = healthAuthorityTechnicalSupportId;
    // Indicates that a user has progressed through the entire registration, and
    // reached the overview page switching them from wizard to spoke navigation
    this.completed = completed;
    this.submittedDate = submittedDate;
    this.approvedDate = approvedDate;
    this.status = status;
    this.id = id ?? 0;
  }

  /**
   * @description
   * Convert structurally typed HealthAuthoritySite to an
   * instance of HealthAuthoritySite.
   */
  public static toHealthAuthoritySite(healthAuthoritySite: HealthAuthoritySiteDto): HealthAuthoritySite | null {
    if (!healthAuthoritySite) {
      return null;
    }

    return new HealthAuthoritySite(
      healthAuthoritySite.healthAuthorityOrganizationId,
      healthAuthoritySite.healthAuthorityVendor,
      healthAuthoritySite.healthAuthorityCareType,
      healthAuthoritySite.siteName,
      healthAuthoritySite.pec,
      healthAuthoritySite.securityGroupCode,
      healthAuthoritySite.physicalAddress,
      healthAuthoritySite.businessHours,
      healthAuthoritySite.remoteUsers,
      healthAuthoritySite.healthAuthorityPharmanetAdministratorId,
      healthAuthoritySite.healthAuthorityTechnicalSupportId,
      healthAuthoritySite.completed,
      healthAuthoritySite.submittedDate,
      healthAuthoritySite.approvedDate,
      healthAuthoritySite.status,
      healthAuthoritySite.id
    );
  }

  /**
   * @description
   * Get a reduced version of a HealthAuthoritySite for
   * creating a new site.
   */
  public forCreate(authorizedUserId: number): HealthAuthoritySiteCreate {
    const healthAuthorityVendorId = this.healthAuthorityVendor?.id;
    if (!authorizedUserId) {
      throw Error('Authorized user identifier was not provided');
    }
    if (!healthAuthorityVendorId) {
      throw Error('Health authority vendor identifier was not provided');
    }

    return {
      authorizedUserId,
      healthAuthorityVendorId
    };
  }

  /**
   * @description
   * Get a reduced version of a HealthAuthoritySite for
   * updating an existing site.
   */
  public forUpdate(): HealthAuthoritySiteUpdate {
    return {
      siteName: this.siteName,
      pec: this.pec,
      securityGroupCode: this.securityGroupCode,
      physicalAddress: { ...this.physicalAddress },
      businessHours: [...this.businessHours],
      remoteUsers: [...this.remoteUsers],
      healthAuthorityPharmanetAdministratorId: this.healthAuthorityPharmanetAdministratorId ?? null,
      healthAuthorityTechnicalSupportId: this.healthAuthorityTechnicalSupportId ?? null,
      healthAuthorityVendorId: this.healthAuthorityVendor?.id ?? null,
      healthAuthorityCareTypeId: this.healthAuthorityCareType?.id ?? null
    };
  }

  public isIncomplete(): boolean {
    return !this.submittedDate || (
      this.submittedDate &&
      !this.approvedDate &&
      this.status === SiteStatusType.EDITABLE
    );
  }

  public isInReview(): boolean {
    return this.status === SiteStatusType.IN_REVIEW;
  }

  public isLocked(): boolean {
    return this.status === SiteStatusType.LOCKED;
  }

  public isApproved(): boolean {
    return this.status === SiteStatusType.EDITABLE && !!this.approvedDate;
  }

  public withinRenewalPeriod(): boolean {
    return DateUtils.withinRenewalPeriod(this.getExpiryDate());
  }

  // TODO do health authority sites need to renew?
  public getExpiryDate(): string | null {
    return (this.submittedDate)
      ? moment(this.submittedDate).add(1, 'year').format()
      : null;
  }
}
