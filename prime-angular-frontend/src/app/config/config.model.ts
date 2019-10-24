// TODO: revisit types, naming, etc; when configuration is more mature
export interface Configuration {
  countries: Config[];
  colleges: CollegeConfig[];
  jobNames: Config[];
  licenses: LicenseConfig[];
  organizationNames: Config[];
  organizationTypes: Config[];
  practices: PracticeConfig[];
  provinces: Config[];
  statuses: Config[];
}

export interface Config {
  code: number;
  name: string;
}

export interface LicenseConfig extends Config {
  collegeLicenses: AssociatedCollegeConfig[];
}

export interface PracticeConfig extends Config {
  collegePractices: AssociatedCollegeConfig[];
}

export interface AssociatedCollegeConfig {
  collegeCode: number;
  [key: string]: number;
}

export interface CollegeConfig extends LicenseConfig, PracticeConfig {
  prefix: string;
}
