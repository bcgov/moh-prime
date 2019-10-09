// TODO: revisit types, naming, etc; when configuration is more mature
export interface Configuration {
  countries: Config[];
  colleges: CollegeConfig[];
  jobNames: Config[];
  licenses: LicenseConfig[];
  organizationNames: Config[];
  organizationTypes: Config[];
  practices: Config[];
  provinces: Config[];
}

export interface Config {
  code: string;
  name: string;
}

export interface CollegeLicenseConfig {
  collegeCode: number;
  licenseCode: number;
}

export interface LicenseConfig extends Config {
  collegeLicenses: CollegeLicenseConfig[];
}

export interface CollegeConfig extends LicenseConfig {
  prefix: string;
}
