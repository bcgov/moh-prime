import { Address } from '@lib/models/address.model';

export interface OboSite {
  careSettingCode: number;
  healthAuthorityCode?: number;
  siteName: string;
  facilityName: string;
  jobTitle: string;
  physicalAddress: Address;
  pec: string;
}
