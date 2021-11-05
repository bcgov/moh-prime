import { Address } from '@lib/models/address.model';

export interface RemoteAccessLocation {
  id?: number;
  internetProvider: string;
  physicalAddress: Address;
}
