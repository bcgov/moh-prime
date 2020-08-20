import { SiteViewModel } from '@registration/shared/models/site.model';
import { OrganizationViewModel } from '@registration/shared/models/organization.model';

/**
 * View model specifically created to combine multiple models to allow use
 * of the Angular Material table, which requires the model hierarchy to be
 * flattened as it manages iterating over the datasource internally.
 *
 * NOTE: should only be used within the SiteRegistrationContainer,
 * SiteRegistrationTable, and SiteRegistrationActions
 */
export interface SiteRegistrationViewModel extends Omit<OrganizationViewModel, 'id'>, Omit<SiteViewModel, 'id'> {
  organizationId: number;
  siteId: number;
}
