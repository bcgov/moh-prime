
import { NgModule, InjectionToken } from '@angular/core';

import { environment } from '../environments/environment';

export let APP_CONFIG = new InjectionToken<AppConfig>('app.config');

export class AppConfig {
  apiEndpoint: string;
  routes: {
    auth: string;
    dashboard: string;
    admin: string;
    provision: string;
    enrolment: string;
    denied: string;
    maintenance: string;
  };
}

export const APP_DI_CONFIG: AppConfig = {
  apiEndpoint: environment.apiEndpoint,
  routes: {
    auth: 'auth',
    dashboard: 'dashboard',
    admin: 'admin',
    provision: 'provision',
    enrolment: 'enrolment',
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
