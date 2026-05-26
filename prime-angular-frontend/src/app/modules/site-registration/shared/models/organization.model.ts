import { Party } from '@lib/models/party.model';
import { Site } from './site.model';

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
  hasClaim: boolean;
  pendingTransfer: boolean;
  isArchived: boolean;
  // Children -----
  sites: Site[];
}

export interface OrganizationAdminView {
  id: number;
  name: string;
  doingBusinessAs?: string;
  signingAuthorityName: string;
  createdDate: string;
  isArchived: boolean;
  hidden: boolean;
  hasClaim: boolean;
  siteId: number;
  validSiteCount: number;
  hasSubmittedSite: boolean;
}
