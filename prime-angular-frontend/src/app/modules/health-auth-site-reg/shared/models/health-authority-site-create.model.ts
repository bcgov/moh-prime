import { HealthAuthoritySite } from '@health-auth/shared/models/health-authority-site.model';

export interface HealthAuthoritySiteCreate extends Pick<HealthAuthoritySite,
  'healthAuthorityVendorId'> {
  authorizedUserId: number;
}
