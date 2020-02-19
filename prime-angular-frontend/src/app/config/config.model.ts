export interface Configuration {
  countries: Config<string>[];
  colleges: CollegeConfig[];
  jobNames: Config<number>[];
  licenses: LicenseWeightedConfig[];
  organizationTypes: Config<number>[];
  practices: PracticeConfig[];
  provinces: ProvinceConfig[];
  statuses: Config<number>[];
  statusReasons: Config<number>[];
  privilegeGroups: PrivilegeGroupConfig[];
  privilegeTypes: Config<number>[];
}

export class Config<T> {
  code: T;
  name: string;

  constructor(code: T, name: string) {
    this.code = code;
    this.name = name;
  }
}

export interface LicenseConfig extends Config<number> {
  collegeLicenses: AssociatedCollegeConfig[];
}

export interface LicenseWeightedConfig extends LicenseConfig {
  weight: number;
}

export interface PracticeConfig extends Config<number> {
  collegePractices: AssociatedCollegeConfig[];
}

export interface ProvinceConfig extends Config<string> {
  countryCode: string;
}

export interface AssociatedCollegeConfig {
  collegeCode: number;
  [key: string]: number;
}

export interface CollegeConfig extends LicenseConfig, PracticeConfig {
  prefix: string;
}

export interface PrivilegeGroupConfig extends Config<number> {
  privilegeTypeCode: number;
}
