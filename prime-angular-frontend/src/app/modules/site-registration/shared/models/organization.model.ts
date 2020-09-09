import { Site, SiteListViewModel } from './site.model';
import { Party } from './party.model';
import { SignedAgreementDocument } from './signed-agreement-document.model';

export interface Organization {
  id?: number;
  displayId?: number;
  sites: Site[];
  siteCount: number;
  // Forms -----
  signingAuthorityId?: number;
  signingAuthority: Party;
  name: string;
  registrationId: string;
  doingBusinessAs?: string;
  // States -----
  completed: boolean;
  acceptedAgreementDate: string;
  signedAgreementDocuments: SignedAgreementDocument[];
  submittedDate: string;
}

export interface OrganizationListViewModel extends
  Omit<Organization, 'sites' | 'siteCount' | 'registrationId' | 'doingBusinessAs' | 'signedAgreementDocuments'> {
  sites: SiteListViewModel[];
  signedAgreementDocumentCount: number;
}
