import { Address } from '@shared/models/address.model';
import { User } from './user.model';

export interface BcscUser extends User {
  hpdid: string; // BCSC GUID
  userId: string; // Keycloak identifier
  email: string;
  firstName: string;
  lastName: string;
  givenNames: string;
  dateOfBirth: string;
  physicalAddress: Address;
}
