// This file can be replaced during build by using the `fileReplacements` array.
// `ng build --configuration=production` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

import { AppEnvironment } from '@env/environment.model';
import { environment as defaultEnvironment } from '@env/environment.prod';

/**
 * @description
 * Development environment populated with the default and
 * production environment with appropriate overrides.
 *
 * WARNING: Do not access environment directly. Config map properties
 * are injected by the pipeline and override the environment defaults
 * used for local development.
 */
export const environment: AppEnvironment = {
  ...defaultEnvironment,
  production: false,
  apiEndpoint: 'http://localhost:5000/api'
};

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/plugins/zone-error';  // Included with Angular CLI.
