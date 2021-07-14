import { HealthAuthoritySite } from '@health-auth/shared/models/health-authority-site.model';

export interface RemoteUsersForm extends Pick<HealthAuthoritySite, 'remoteUsers'> {}
