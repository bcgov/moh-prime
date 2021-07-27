import { SiteListViewModel } from '@registration/shared/models/site.model';
import { Organization } from '@registration/shared/models/organization.model';

export interface SiteListViewModelPartial extends
  Omit<SiteListViewModel, 'id' | 'completed' | 'doingBusinessAs'> {
  siteId: number;
  siteDoingBusinessAs: string;
  flagged: boolean;
}

export interface OrganizationListViewModelPartial extends
  Omit<Organization, 'id' | 'sites' | 'completed' | 'doingBusinessAs' | 'hasSubmittedSite' | 'hasAcceptedAgreement' | 'registrationId'> {
  organizationId: number;
  organizationDoingBusinessAs: string;
}

export interface OrganizationSearchListViewModel {
  matchOn: string[];
  organization: Organization;
}

/**
 * View model specifically created to combine multiple models to allow use
 * of the Angular Material table, which requires the model hierarchy to be
 * flattened as it manages iterating over the datasource internally.
 *
 * NOTE: should only be used within the SiteRegistrationContainer,
 * SiteRegistrationTable, and SiteRegistrationActions
 */
export interface SiteRegistrationListViewModel extends OrganizationListViewModelPartial, SiteListViewModelPartial {
  // Only exists when viewing a list of site registrations, and
  // populated by the OrganizationSearchListViewModel
  matchOn?: string[];
}
