import { Address } from '@lib/models/address.model';
import { User } from './user.model';

export interface BcscUser extends User {
  hpdid: string; // BCSC ID
  userId: string; // Keycloak identifier
  username: string; // e.g. gtcochh2vajdtodkby27kspv554dn4is@bcsc
  givenNames: string;
  dateOfBirth: string;
  verifiedAddress: Address;
}
