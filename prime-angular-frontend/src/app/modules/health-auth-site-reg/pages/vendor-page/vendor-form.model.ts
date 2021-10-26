import { HealthAuthoritySite } from '@health-auth/shared/models/health-authority-site.model';

export interface VendorForm extends Pick<HealthAuthoritySite, 'healthAuthorityVendorId'> {}
