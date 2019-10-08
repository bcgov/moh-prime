export interface Config {
  advancedPractices: ConfigKeyValue[];
  countries: ConfigKeyValue[];
  colleges: ConfigKeyValue[];
  jobNames: ConfigKeyValue[];
  licenses: ConfigKeyValue[];
  organizationNames: ConfigKeyValue[];
  organizationTypes: ConfigKeyValue[];
  practices: ConfigKeyValue[];
  provinces: ConfigKeyValue[];
}

export interface ConfigKeyValue {
  code: string;
  name: string;
  [key: string]: any;
}
