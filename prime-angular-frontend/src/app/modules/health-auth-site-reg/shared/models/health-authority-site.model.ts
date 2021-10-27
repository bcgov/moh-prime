import { NonFunctionProperties } from '@lib/types';
import { Address } from '@lib/models/address.model';
import { RemoteUser } from '@lib/models/remote-user.model';
import { BusinessDay } from '@lib/models/business-day.model';
import { SiteStatusType } from '@lib/enums/site-status.enum';
import { HealthAuthorityEnum } from '@lib/enums/health-authority.enum';
import { ObjectUtils } from '@lib/utils/object-utils.class';

import { HealthAuthorityVendor } from '@health-auth/shared/models/health-authority-vendor.model';
import { HealthAuthorityCareType } from '@health-auth/shared/models/health-authority-care-type.model';
import { HealthAuthoritySiteUpdate } from '@health-auth/shared/models/health-authority-site-update.model';
import { HealthAuthoritySiteCreate } from '@health-auth/shared/models/health-authority-site-create.model';

// TODO reverse the implementation so HealthAuthoritySite extends the DTO and adds methods
export interface HealthAuthoritySiteDto extends NonFunctionProperties<HealthAuthoritySite> {}

export class HealthAuthoritySite {
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

  public asDto(): HealthAuthoritySiteDto {
    return {
      healthAuthorityOrganizationId: this.healthAuthorityOrganizationId,
      healthAuthorityVendor: this.healthAuthorityVendor,
      healthAuthorityCareType: this.healthAuthorityCareType,
      siteName: this.siteName,
      pec: this.pec,
      securityGroupCode: this.securityGroupCode,
      physicalAddress: this.physicalAddress,
      businessHours: this.businessHours,
      remoteUsers: this.remoteUsers,
      healthAuthorityPharmanetAdministratorId: this.healthAuthorityPharmanetAdministratorId,
      healthAuthorityTechnicalSupportId: this.healthAuthorityTechnicalSupportId,
      completed: this.completed,
      submittedDate: this.submittedDate,
      approvedDate: this.approvedDate,
      status: this.status,
      id: this.id
    };
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

    return Object.freeze({
      authorizedUserId,
      healthAuthorityVendorId
    });
  }

  /**
   * @description
   * Get a reduced version of a HealthAuthoritySite for
   * updating an existing site.
   */
  public forUpdate(): HealthAuthoritySiteUpdate {
    return ObjectUtils.deepFreeze({
      pec: this.pec,
      securityGroupCode: this.securityGroupCode,
      physicalAddress: this.physicalAddress,
      businessHours: this.businessHours,
      remoteUsers: this.remoteUsers,
      healthAuthorityPharmanetAdministratorId: this.healthAuthorityPharmanetAdministratorId ?? 0,
      healthAuthorityTechnicalSupportId: this.healthAuthorityTechnicalSupportId ?? 0,
      healthAuthorityVendorId: this.healthAuthorityVendor?.id ?? 0,
      healthAuthorityCareTypeId: this.healthAuthorityCareType?.id ?? 0
    });
  }
}
