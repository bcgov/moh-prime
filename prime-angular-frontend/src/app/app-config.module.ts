import { NgModule, InjectionToken } from '@angular/core';

import { AppRoutes } from './app.routes';

import { environment } from '@env/environment';
import { AppEnvironment } from '@env/environment.model';

import { AuthRoutes } from '@auth/auth.routes';
import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { SiteRoutes } from '@registration/site-registration.routes';
import { PhsaEformsRoutes } from '@phsa/phsa-eforms.routes';
import { GisEnrolmentRoutes } from '@gis/gis-enrolment.routes';
import { PaperEnrolmentRoutes } from '@paper-enrolment/paper-enrolment.routes';

export let APP_CONFIG = new InjectionToken<AppConfig>('app.config');

export class AppConfig extends AppEnvironment {
  routes: {
    denied: string;
    maintenance: string;
    auth: string;
    enrolment: string;
    adjudication: string;
    site: string;
    phsa: string;
    gis: string;
    paperEnrolment: string;
  };
}

// Default application configuration is for local development
// outside and inside a container based on environment.
export const APP_DI_CONFIG: AppConfig = {
  ...environment,
  routes: {
    denied: AppRoutes.DENIED,
    maintenance: AppRoutes.MAINTENANCE,
    auth: AuthRoutes.MODULE_PATH,
    enrolment: EnrolmentRoutes.MODULE_PATH,
    adjudication: AdjudicationRoutes.MODULE_PATH,
    site: SiteRoutes.MODULE_PATH,
    phsa: PhsaEformsRoutes.MODULE_PATH,
    gis: GisEnrolmentRoutes.MODULE_PATH,
    paperEnrolment: PaperEnrolmentRoutes.MODULE_PATH
  }
};

@NgModule({})
export class AppConfigModule {}
