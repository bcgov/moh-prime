import { Address } from '@shared/models/address.model';

export interface OboSite {
  careSettingCode: number;
  siteName: string;
  facilityName: string;
  physicalAddress: Address;
  pec: string;
}
