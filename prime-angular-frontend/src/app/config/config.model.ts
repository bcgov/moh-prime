import { PrescriberIdTypeEnum } from '@shared/enums/prescriber-id-type.enum';

export interface Configuration {
  practices: PracticeConfig[];
  colleges: CollegeConfig[];
  countries: Config<string>[];
  jobNames: Config<number>[];
  licenses: LicenseConfig[];
  careSettings: Config<number>[];
  provinces: ProvinceConfig[];
  statuses: Config<number>[];
  statusReasons: Config<number>[];
  vendors: VendorConfig[];
  healthAuthorities: Config<number>[];
  facilities: Config<number>[];
}

export class Config<T> {
  code: T;
  name: string;

  constructor(code: T, name: string) {
    this.code = code;
    this.name = name;
  }
}

export interface CollegeConfig extends PracticeConfig {
  collegeLicenses: CollegeLicenseConfig[];
}

export interface PracticeConfig extends Config<number> {
  collegePractices: CollegePracticeConfig[];
}

export interface LicenseConfig extends Config<number> {
  prefix: string;
  collegeLicenses: CollegeLicenseConfig[];
  licensedToProvideCare: boolean;
  namedInImReg: boolean;
  weight: number;
  validate: boolean;
  prescriberIdType: PrescriberIdTypeEnum;
}

export interface CollegeLicenseConfig {
  collegeCode: number;
  licenseCode: number;
}

export interface CollegePracticeConfig {
  collegeCode: number;
  practiceCode: number;
}

export interface VendorConfig extends Config<number> {
  careSettingCode: number;
}

export interface ProvinceConfig extends Config<string> {
  countryCode: string;
}
