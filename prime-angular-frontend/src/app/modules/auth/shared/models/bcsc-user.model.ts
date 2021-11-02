import { Address } from '@lib/models/address.model';
import { User } from './user.model';

export interface BcscUser extends User {
  hpdid: string; // BCSC GUID
  userId: string; // Keycloak identifier
  givenNames: string;
  dateOfBirth: string;
  verifiedAddress: Address;
}
