
import { NgModule, InjectionToken } from '@angular/core';

import { environment } from '../environments/environment';
import { AppRoutes } from './app.routes';
import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { AdjudicationRoutes } from '@adjudication/adjudication.routes';

export let APP_CONFIG = new InjectionToken<AppConfig>('app.config');

export class AppConfig {
  apiEndpoint: string;
  loginRedirectUrl: string;
  prime: {
    phone: string;
    email: string;
  };
  routes: {
    auth: string;
    enrolment: string;
    adjudication: string;
    denied: string;
    maintenance: string;
  };
}

export const APP_DI_CONFIG: AppConfig = {
  apiEndpoint: environment.apiEndpoint,
  loginRedirectUrl: environment.loginRedirectUrl,
  // TODO move back into environment file when Paul is available
  prime: {
    phone: '1-844-39PRIME',
    email: 'prime@gov.bc.ca',
  },
  routes: {
    auth: EnrolmentRoutes.MODULE_PATH,
    enrolment: EnrolmentRoutes.MODULE_PATH,
    adjudication: AdjudicationRoutes.MODULE_PATH,
    denied: AppRoutes.DENIED,
    maintenance: AppRoutes.MAINTENANCE
  }
};

@NgModule({
  providers: [{
    provide: APP_CONFIG,
    useValue: APP_DI_CONFIG
  }]
})
export class AppConfigModule { }
