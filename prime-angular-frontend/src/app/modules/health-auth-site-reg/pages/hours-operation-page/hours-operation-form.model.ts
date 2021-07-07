import { HealthAuthoritySite } from '@health-auth/shared/models/health-authority-site.model';

export interface HoursOperationForm extends Pick<HealthAuthoritySite, 'businessHours'> {}
