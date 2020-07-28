import { Party } from './party.model';
import { SignedAgreementDocument } from './signed-agreement-document.model';

export interface Organization {
  id?: number;
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
