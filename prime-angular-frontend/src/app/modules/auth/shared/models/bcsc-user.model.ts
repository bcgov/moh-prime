import { Address } from '@lib/models/address.model';
import { User } from './user.model';

export interface BcscUser extends User {
  hpdid: string; // BCSC GUID, e.g. gtcochh2vajdtodkby27kspv554dn4is
  userId: string; // Keycloak identifier, e.g. 0edecf21-1ec1-4e2a-9878-2fddd7c536f1
  username: string; // e.g. gtcochh2vajdtodkby27kspv554dn4is@bcsc
  givenNames: string;
  dateOfBirth: string;
  verifiedAddress: Address;
}
