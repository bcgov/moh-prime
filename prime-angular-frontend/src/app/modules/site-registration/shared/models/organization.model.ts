import { Party } from './party.model';

export interface Organization {
  signingAuthorityId: number;
  signingAuthority: Party;
  name: string;
  doingBusinessAs?: string;
  acceptedAgreementDate: string;
}
