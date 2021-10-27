import { HealthAuthoritySite } from '@health-auth/shared/models/health-authority-site.model';

export interface HealthAuthoritySiteUpdate extends Pick<HealthAuthoritySite,
  'pec' |
  'securityGroupCode' |
  'physicalAddress' |
  'businessHours' |
  'remoteUsers' |
  'healthAuthorityPharmanetAdministratorId' |
  'healthAuthorityTechnicalSupportId'> {
  healthAuthorityVendorId: number;
  healthAuthorityCareTypeId: number;
}


