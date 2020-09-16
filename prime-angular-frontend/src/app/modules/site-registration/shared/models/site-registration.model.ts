import { SiteListViewModel } from '@registration/shared/models/site.model';
import { OrganizationListViewModel } from '@registration/shared/models/organization.model';


export interface SiteListViewModelPartial extends Omit<SiteListViewModel, 'id' | 'completed'> {
  siteId: number;
}

export interface OrganizationListViewModelPartial extends Omit<OrganizationListViewModel, 'id' | 'sites' | 'completed'> {
  organizationId: number;
}

/**
 * View model specifically created to combine multiple models to allow use
 * of the Angular Material table, which requires the model hierarchy to be
 * flattened as it manages iterating over the datasource internally.
 *
 * NOTE: should only be used within the SiteRegistrationContainer,
 * SiteRegistrationTable, and SiteRegistrationActions
 */
export interface SiteRegistrationListViewModel extends OrganizationListViewModelPartial, SiteListViewModelPartial { }
