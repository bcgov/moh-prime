import { Party } from './party.model';
import { SignedAgreementDocument } from './signed-agreement-document.model';

export interface Organization {
  id?: number;

  signingAuthorityId?: number;
  signingAuthority: Party;
  name: string;
  registrationId: string;
  doingBusinessAs?: string;

  completed: boolean;
  acceptedAgreementDate: string;
  signedAgreementDocuments: SignedAgreementDocument[];
  submittedDate: string;
}
