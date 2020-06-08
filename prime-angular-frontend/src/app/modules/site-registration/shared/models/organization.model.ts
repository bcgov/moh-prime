import { Party } from './party.model';
import { Location } from './location.model';

export interface Organization {
  id?: number;
  signingAuthorityId?: number;
  signingAuthority: Party;
  name: string;
  registrationId: number;
  doingBusinessAs?: string;
  organizationTypeCode: number;
  acceptedAgreementDate: string;
  locations: Location[];
  submittedDate: string;
  completed: boolean;
}
