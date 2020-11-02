import { Address } from '@shared/models/address.model';

export interface RemoteAccessLocation {
  id?: number;
  internetProvider: string;
  physicalAddress: Address;
}
