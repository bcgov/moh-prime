import { HealthAuthoritySite } from '@health-auth/shared/models/health-authority-site.model';
import { Address } from '@lib/models/address.model';

export interface SiteInformationForm extends Pick<HealthAuthoritySite, 'siteName' | 'pec' | 'securityGroupCode' | 'mnemonic' | 'physicalAddress'> { }
