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
  careTypes: Config<number>[];
  securityGroups: Config<number>[];
  collegeLicenseGroupings: CollegeLicenseGroupingConfig[];
  deviceProviderRoles: DeviceProviderRoleConfig[];
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
  weight: number;
}

export interface PracticeConfig extends Config<number> {
  collegePractices: CollegePracticeConfig[];
}

export class LicenseConfig extends Config<number> implements IWeightedConfig {
  prefix: string;
  collegeLicenses: CollegeLicenseConfig[];
  licensedToProvideCare: boolean;
  namedInImReg: boolean;
  allowRequestRemoteAccess: boolean;
  weight: number;
  validate: boolean;
  manual: boolean;
  prescriberIdType: PrescriberIdTypeEnum;
  multijurisdictional: boolean;
  remoteAccessTypeLicenses: RemoteAccessTypeLicense[];
}

export interface CollegeLicenseConfig {
  collegeCode: number;
  licenseCode: number;
  collegeLicenseGroupingCode: number;
  discontinued: boolean;
}

export interface RemoteAccessTypeLicense {
  remoteAccessTypeCode: number;
}

export class CollegeLicenseGroupingConfig extends Config<number> implements IWeightedConfig {
  code: number;
  name: string;
  weight: number;
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

export interface IWeightedConfig {
  weight: number;
}

export interface DeviceProviderRoleConfig extends Config<number> {
  deviceProviderRoleCode: number;
  certified: boolean;
  weight: number;
}
