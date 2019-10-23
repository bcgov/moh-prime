
import { NgModule, InjectionToken } from '@angular/core';

import { environment } from '../environments/environment';

export let APP_CONFIG = new InjectionToken<AppConfig>('app.config');

export class AppConfig {
  apiEndpoint: string;
  routes: {
    auth: string;
    admin: string;
    enrolment: string;
    provision: string;
    denied: string;
    maintenance: string;
  };
}

export const APP_DI_CONFIG: AppConfig = {
  apiEndpoint: environment.apiEndpoint,
  routes: {
    auth: '',
    admin: 'admin',
    enrolment: 'enrolment',
    provision: 'provision',
    denied: 'denied',
    maintenance: 'maintenance'
  }
};

@NgModule({
  providers: [{
    provide: APP_CONFIG,
    useValue: APP_DI_CONFIG
  }]
})
export class AppConfigModule { }
