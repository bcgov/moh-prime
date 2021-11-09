import { HealthAuthoritySite } from '@health-auth/shared/models/health-authority-site.model';

export interface SiteInformationForm extends Pick<HealthAuthoritySite, 'siteName' | 'pec' | 'securityGroupCode' | 'id'> { }
