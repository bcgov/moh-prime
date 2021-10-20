import { HealthAuthoritySite } from '@health-auth/shared/models/health-authority-site.model';

export interface HealthAuthoritySiteUpdate extends Pick<HealthAuthoritySite,
  'healthAuthorityVendorCode' |
  'healthAuthorityCareTypeCode' |
  'siteName' |
  'siteId' |
  'securityGroupCode' |
  'physicalAddress' |
  'businessHours' |
  'remoteUsers' |
  'healthAuthorityPharmanetAdministratorId' |
  'healthAuthorityTechnicalSupportId'> {}
