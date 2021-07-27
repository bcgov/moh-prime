import { Party } from '@lib/models/party.model';
import { Site, SiteListViewModel } from './site.model';

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
  sites: Site[];
}
