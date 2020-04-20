import { Party } from './party.model';

export interface Organization {
  id: number;
  signingAuthorityId: number;
  signingAuthority: Party;
  name: string;
  doingBusinessAs?: string;
  acceptedAgreementDate: string;
}
