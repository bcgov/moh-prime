import { Address } from '@lib/models/address.model';

export interface RemoteAccessSearch {
  remoteUserId: number;
  siteId: number;
  siteAddress: Address;
  siteDoingBusinessAs: string;
  vendorCodes: number;
}
