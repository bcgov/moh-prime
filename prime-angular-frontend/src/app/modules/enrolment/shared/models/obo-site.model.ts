import { Address } from '@shared/models/address.model';

export interface OboSite {
  siteName: string;
  facility: string;
  physicalAddress: Address;
  pec: string;
}
