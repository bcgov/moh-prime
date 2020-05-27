import { Address } from '@shared/models/address.model';

export interface RemoteUserLocation {
  internetProvider: string;
  physicalAddress: Address;
}
