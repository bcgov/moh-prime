import { HealthAuthorityEnum } from '@lib/enums/health-authority.enum';
import { SiteStatusType } from '@lib/enums/site-status.enum';
import { Address } from '@lib/models/address.model';
import { BusinessDay } from '@lib/models/business-day.model';
import { AbstractBaseHealthAuthoritySite } from './abstract-base-health-authority-site.class';
import { BaseHealthAuthoritySite } from './base-health-authority-site.model';
import { HealthAuthorityCareType } from './health-authority-care-type.model';
import { HealthAuthorityVendor } from './health-authority-vendor.model';

export interface HealthAuthorityAdminSiteDto extends BaseHealthAuthoritySite {
  healthAuthorityVendor: HealthAuthorityVendor;
  healthAuthorityCareType: HealthAuthorityCareType;
  siteName;
  pec: string;
  securityGroupCode: number;
  physicalAddress: Address;
  businessHours: BusinessDay[];
  healthAuthorityPharmanetAdministratorId: number;
  healthAuthorityTechnicalSupportId: number;
  readonly pharmanetAdministratorName: string;
  readonly technicalSupportName: string;
}

export class HealthAuthoritySiteAdmin extends AbstractBaseHealthAuthoritySite implements HealthAuthorityAdminSiteDto {
  constructor(
    public id: number,
    public healthAuthorityOrganizationId: HealthAuthorityEnum,
    public healthAuthorityVendor: HealthAuthorityVendor,
    public healthAuthorityCareType: HealthAuthorityCareType,
    public siteName: string,
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

    public readonly pharmanetAdministratorName: string,
    public readonly technicalSupportName: string
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
    this.pharmanetAdministratorName = pharmanetAdministratorName;
    this.technicalSupportName = technicalSupportName;
  }
}
