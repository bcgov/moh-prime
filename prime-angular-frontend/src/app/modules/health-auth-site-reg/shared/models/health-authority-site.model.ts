import { Address } from '@lib/models/address.model';
import { BusinessDay } from '@lib/models/business-day.model';
import { SiteStatusType } from '@lib/enums/site-status.enum';
import { HealthAuthorityEnum } from '@lib/enums/health-authority.enum';

import { BaseHealthAuthoritySite } from '@health-auth/shared/models/base-health-authority-site.model';
import { AbstractBaseHealthAuthoritySite } from '@health-auth/shared/models/abstract-base-health-authority-site.class';
import { HealthAuthorityVendor } from '@health-auth/shared/models/health-authority-vendor.model';
import { HealthAuthorityCareType } from '@health-auth/shared/models/health-authority-care-type.model';
import { HealthAuthoritySiteUpdate } from '@health-auth/shared/models/health-authority-site-update.model';
import { HealthAuthoritySiteCreate } from '@health-auth/shared/models/health-authority-site-create.model';
import { SiteSubmission } from '@shared/models/site-submission.model';

// TODO split up Site, CommunitySite, and HealthAuthoritySite into separate interfaces/classes
export interface HealthAuthoritySiteDto extends BaseHealthAuthoritySite {
  healthAuthorityVendor: HealthAuthorityVendor;
  healthAuthorityCareType: HealthAuthorityCareType;
  siteName: string;
  mnemonic: string;
  pec: string;
  securityGroupCode: number;
  physicalAddress: Address;
  businessHours: BusinessDay[];
  healthAuthorityPharmanetAdministratorId: number;
  healthAuthorityTechnicalSupportId: number;
  currentSubmission: SiteSubmission;
}

export class HealthAuthoritySite extends AbstractBaseHealthAuthoritySite implements HealthAuthoritySiteDto {
  constructor(
    public id: number,
    public healthAuthorityOrganizationId: HealthAuthorityEnum,
    public healthAuthorityVendor: HealthAuthorityVendor,
    public healthAuthorityCareType: HealthAuthorityCareType,
    public siteName: string,
    public mnemonic: string,
    public pec: string,
    public securityGroupCode: number,
    public physicalAddress: Address,
    public businessHours: BusinessDay[],
    public healthAuthorityPharmanetAdministratorId: number,
    public healthAuthorityTechnicalSupportId: number,
    public readonly completed: boolean,
    public readonly submittedDate: string,
    public readonly approvedDate: string,
    public readonly status: SiteStatusType,
    public readonly currentSubmission: SiteSubmission,
  ) {
    super(id, healthAuthorityOrganizationId, completed, submittedDate, approvedDate, status);

    this.healthAuthorityVendor = healthAuthorityVendor;
    this.healthAuthorityCareType = healthAuthorityCareType;
    this.siteName = siteName;
    this.pec = pec;
    this.securityGroupCode = securityGroupCode;
    this.physicalAddress = physicalAddress;
    this.businessHours = businessHours;
    this.healthAuthorityPharmanetAdministratorId = healthAuthorityPharmanetAdministratorId;
    this.healthAuthorityTechnicalSupportId = healthAuthorityTechnicalSupportId;
    this.currentSubmission = currentSubmission;
  }

  /**
   * @description
   * Convert structurally typed HealthAuthoritySiteDto to an
   * instance of HealthAuthoritySite.
   */
  public static toHealthAuthoritySite(healthAuthoritySite: HealthAuthoritySiteDto): HealthAuthoritySite | null {
    if (!healthAuthoritySite) {
      return null;
    }

    return new HealthAuthoritySite(
      healthAuthoritySite.id,
      healthAuthoritySite.healthAuthorityOrganizationId,
      healthAuthoritySite.healthAuthorityVendor,
      healthAuthoritySite.healthAuthorityCareType,
      healthAuthoritySite.siteName,
      healthAuthoritySite.mnemonic,
      healthAuthoritySite.pec,
      healthAuthoritySite.securityGroupCode,
      healthAuthoritySite.physicalAddress,
      healthAuthoritySite.businessHours,
      healthAuthoritySite.healthAuthorityPharmanetAdministratorId,
      healthAuthoritySite.healthAuthorityTechnicalSupportId,
      healthAuthoritySite.completed,
      healthAuthoritySite.submittedDate,
      healthAuthoritySite.approvedDate,
      healthAuthoritySite.status,
      healthAuthoritySite.currentSubmission
    );
  }

  /**
   * @description
   * Get a reduced version of a HealthAuthoritySite for
   * creating a new site.
   */
  public forCreate(authorizedUserId: number): HealthAuthoritySiteCreate {
    const healthAuthorityCareTypeId = this.healthAuthorityCareType?.id;
    const healthAuthorityVendorId = this.healthAuthorityVendor?.id;
    if (!authorizedUserId) {
      throw Error('Authorized user identifier was not provided');
    }
    if (!healthAuthorityCareTypeId) {
      throw Error('Health Authority Care Type was not provided');
    }

    return {
      authorizedUserId,
      healthAuthorityCareTypeId,
      healthAuthorityVendorId,
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
      mnemonic: this.mnemonic,
      securityGroupCode: this.securityGroupCode,
      physicalAddress: { ...this.physicalAddress },
      businessHours: [...this.businessHours],
      healthAuthorityPharmanetAdministratorId: this.healthAuthorityPharmanetAdministratorId ?? null,
      healthAuthorityTechnicalSupportId: this.healthAuthorityTechnicalSupportId ?? null,
      healthAuthorityVendorId: this.healthAuthorityVendor?.id ?? null,
      healthAuthorityCareTypeId: this.healthAuthorityCareType?.id ?? null
    };
  }
}
