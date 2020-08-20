import { Site, SiteViewModel } from './site.model';
import { Party } from './party.model';
import { SignedAgreementDocument } from './signed-agreement-document.model';

export interface Organization {
  id?: number;
  displayId?: number;
  sites: Site[];
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
  siteCount: number;
}

export interface OrganizationViewModel extends Pick<Organization, 'id' | 'displayId' | 'name' | 'signingAuthorityId' | 'signingAuthority'> {
  sites: SiteViewModel[];
}
