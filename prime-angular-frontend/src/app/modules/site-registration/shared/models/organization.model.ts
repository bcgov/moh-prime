import { Party } from './party.model';
import { Location } from './location.model';
import { SignedAgreementDocument } from './signed-agreement-document.model';

export interface Organization {
  id?: number;
  signingAuthorityId?: number;
  signingAuthority: Party;
  name: string;
  registrationId: string;
  doingBusinessAs?: string;
  organizationTypeCode: number;
  acceptedAgreementDate: string;
  locations: Location[];
  submittedDate: string;
  completed: boolean;
  signedAgreementDocuments: SignedAgreementDocument[];
}
