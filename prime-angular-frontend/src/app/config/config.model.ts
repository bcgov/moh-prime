export interface Config {
  advancedPractices: ConfigKeyValue[];
  colleges: ConfigKeyValue[];
  countries: ConfigKeyValue[];
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
}
