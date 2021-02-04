import { SiteListViewModel } from './site.model';
import { Party } from './party.model';

export interface Organization {
  id?: number;
  displayId?: number;
  registrationId: string;
  // Forms -----
  signingAuthorityId?: number;
  signingAuthority: Party;
  name: string;
  doingBusinessAs?: string;
  // States -----
  completed: boolean;
  hasAcceptedAgreement: boolean;
  hasSubmittedSite: boolean;
  // Children -----
  sites: SiteListViewModel[];
}
