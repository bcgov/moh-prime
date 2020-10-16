import { SiteListViewModel } from './site.model';
import { Party } from './party.model';
import { OrganizationAgreement } from '@shared/models/agreement.model';

export interface Organization {
  id?: number;
  displayId?: number;
  siteCount: number;
  agreements: OrganizationAgreement[];
  // Forms -----
  signingAuthorityId?: number;
  signingAuthority: Party;
  name: string;
  registrationId: string;
  doingBusinessAs?: string;
  // States -----
  completed: boolean;
  submittedDate: string;
  hasAcceptedAgreement: boolean;
}

export interface OrganizationListViewModel extends
  Omit<Organization, 'siteCount' | 'registrationId' | 'hasAcceptedAgreement'> {
  sites: SiteListViewModel[];
  // TODO PRIME-1085 (is this still needed?)
  signedAgreementDocumentCount: number;
}
