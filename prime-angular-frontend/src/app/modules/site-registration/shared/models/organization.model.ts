import { Site, SiteListViewModel } from './site.model';
import { Party } from './party.model';
import { SignedAgreementDocument } from './signed-agreement-document.model';

export interface Organization {
  id?: number;
  displayId?: number;
  siteCount: number;
  // Forms -----
  signingAuthorityId?: number;
  signingAuthority: Party;
  name: string;
  registrationId: string;
  doingBusinessAs?: string;
  // States -----
  completed: boolean;
  submittedDate: string;
}

export interface OrganizationListViewModel extends
  Omit<Organization, 'siteCount' | 'registrationId'> {
  sites: SiteListViewModel[];
  signedAgreementDocumentCount: number;
}
