import { SiteViewModel } from '@registration/shared/models/site.model';
import { OrganizationViewModel } from '@registration/shared/models/organization.model';

// TODO add a comment on why this exists
export interface SiteRegistrationViewModel extends Omit<OrganizationViewModel, 'id'>, Omit<SiteViewModel, 'id'> {
  organizationId: number;
  siteId: number;
}
