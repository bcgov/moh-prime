import { SiteListViewModel } from './site.model';
import { Party } from './party.model';

export interface Organization {
  id?: number;
  displayId?: number;
  // Forms -----
  signingAuthorityId?: number;
  signingAuthority: Party;
  name: string;
  registrationId: string;
  doingBusinessAs?: string;
  // States -----
  completed: boolean;
  hasAcceptedAgreement: boolean;
  hasSubmittedSite: boolean;
}

export interface OrganizationListViewModel extends
  Omit<Organization, 'siteCount' | 'registrationId' | 'hasAcceptedAgreement' | 'hasSubmittedSite'> {
  sites: SiteListViewModel[];
}
