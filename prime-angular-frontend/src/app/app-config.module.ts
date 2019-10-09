
import { NgModule, InjectionToken } from '@angular/core';

import { environment } from '../environments/environment';

export let APP_CONFIG = new InjectionToken<AppConfig>('app.config');

export class AppConfig {
  apiEndpoint: string;
  routes: {
    auth: string;
    dashboard: string;
    admin: string;
    applicant: string;
    denied: string;
  };
}

export const APP_DI_CONFIG: AppConfig = {
  apiEndpoint: environment.apiEndpoint,
  routes: {
    auth: 'auth',
    dashboard: 'dashboard',
    admin: '/dashboard/admin',
    applicant: '/dashboard/applicant',
    denied: 'denied'
  }
};

@NgModule({
  providers: [{
    provide: APP_CONFIG,
    useValue: APP_DI_CONFIG
  }]
})
export class AppConfigModule { }
