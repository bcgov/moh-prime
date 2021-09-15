import { ConfigMap } from '@env/config-map.model';

export type environmentName = 'prod' | 'test' | 'dev' | 'local';

export class AppEnvironment extends ConfigMap {
  // Only indicates that Angular has been built
  // using --configuration=production
  production: boolean;
  version: string;
  prime: {
    displayPhone: string;
    phone: string;
    email: string;
    supportEmail: string;
  };
  phoneNumbers: {
    director: string;
  };
}
