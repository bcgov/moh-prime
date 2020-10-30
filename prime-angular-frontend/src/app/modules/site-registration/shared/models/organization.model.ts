import { SiteListViewModel } from './site.model';
import { Party } from './party.model';
import { OrganizationAgreement } from '@shared/models/agreement.model';

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
  Omit<Organization, 'siteCount' | 'registrationId' | 'hasAcceptedAgreement'> {
  sites: SiteListViewModel[];
}
