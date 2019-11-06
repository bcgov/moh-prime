// TODO: revisit types, naming, etc; when configuration is more mature
export interface Configuration {
  countries: Config<string>[];
  colleges: CollegeConfig[];
  jobNames: Config<number>[];
  licenses: LicenseConfig[];
  organizationNames: Config<number>[];
  organizationTypes: Config<number>[];
  practices: PracticeConfig[];
  provinces: Config<string>[];
  statuses: Config<number>[];
  statusReasons: Config<number>[];
}

export interface Config<T> {
  code: T;
  name: string;
}

export interface LicenseConfig extends Config<number> {
  collegeLicenses: AssociatedCollegeConfig[];
}

export interface PracticeConfig extends Config<number> {
  collegePractices: AssociatedCollegeConfig[];
}

export interface AssociatedCollegeConfig {
  collegeCode: number;
  [key: string]: number;
}

export interface CollegeConfig extends LicenseConfig, PracticeConfig {
  prefix: string;
}
