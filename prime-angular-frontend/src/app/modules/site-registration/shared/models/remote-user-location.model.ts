import { Address } from '@shared/models/address.model';

export interface RemoteUserLocation {
  id?: number;
  internetProvider: string;
  physicalAddress: Address;
}
