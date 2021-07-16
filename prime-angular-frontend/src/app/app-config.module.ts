import { NgModule, InjectionToken } from '@angular/core';

import { AppRoutes } from './app.routes';

import { environment } from '@env/environment';
import { AuthRoutes } from '@auth/auth.routes';
import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { SiteRoutes } from '@registration/site-registration.routes';
import { PhsaEformsRoutes } from '@phsa/phsa-eforms.routes';
import { GisEnrolmentRoutes } from '@gis/gis-enrolment.routes';
import { PaperEnrolmentRoutes } from '@paper-enrolment/paper-enrolment.routes';

export let APP_CONFIG = new InjectionToken<AppConfig>('app.config');
export type environmentName = 'prod' | 'test' | 'dev' | 'local';

export class AppConfig {
  apiEndpoint: string;
  loginRedirectUrl: string;
  documentManagerUrl: string;
  prime: {
    displayPhone: string;
    phone: string;
    email: string;
    supportEmail: string;
  };
  phoneNumbers: {
    director: string;
  };
  environmentName: string;
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

export interface ConfigMap {
  environmentName: string;
  apiEndpoint: string;
  loginRedirectUrl: string;
  documentManagerUrl: string;
  keycloakConfig: {
    config: {
      url: string;
      realm: string;
      clientId: string;
    }
  };
}

export const APP_DI_CONFIG: AppConfig = {
  apiEndpoint: environment.apiEndpoint,
  loginRedirectUrl: environment.loginRedirectUrl,
  documentManagerUrl: environment.documentManagerUrl,
  prime: {
    displayPhone: environment.prime.displayPhone,
    phone: environment.prime.phone,
    email: environment.prime.email,
    supportEmail: environment.prime.supportEmail,
  },
  phoneNumbers: {
    director: environment.phoneNumbers.director
  },
  environmentName: environment.environmentName as environmentName,
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

export const defaultAppConfig = {
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
