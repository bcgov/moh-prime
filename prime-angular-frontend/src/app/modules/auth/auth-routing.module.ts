import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { BannerLocationCode } from '@shared/enums/banner-location-code.enum';

import { AuthRoutes } from './auth.routes';
import { AuthorizationRedirectGuard } from './shared/guards/authorization-redirect.guard';
import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { SiteRoutes } from '@registration/site-registration.routes';
import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { PhsaEformsRoutes } from '@phsa/phsa-eforms.routes';
import { SatEformsRoutes } from '@sat/sat-eforms.routes';
import { GisEnrolmentRoutes } from '@gis/gis-enrolment.routes';
import { GisAuthorizationRedirectGuard } from '@gis/shared/guards/gis-authorization-redirect.guard';
import { HealthAuthSiteRegRoutes } from '@health-auth/health-auth-site-reg.routes';

const routes: Routes = [
  {
    path: AuthRoutes.MODULE_PATH,
    children: [
      {
        path: EnrolmentRoutes.BCSC_LOGIN,
        canLoad: [AuthorizationRedirectGuard],
        data: {
          locationCode: BannerLocationCode.ENROLMENT_LANDING_PAGE
        },
        loadChildren: () => import('@enrolment/shared/modules/bcsc-enrolment-login-page/bcsc-enrolment-login-page.module')
          .then(m => m.BcscEnrolmentLoginPageModule)
      },
      {
        path: EnrolmentRoutes.BCEID_LOGIN,
        canLoad: [AuthorizationRedirectGuard],
        loadChildren: () => import('@enrolment/shared/modules/bceid-enrolment-login-page/bceid-enrolment-login-page.module')
          .then(m => m.BceidEnrolmentLoginPageModule)
      },
      {
        path: SiteRoutes.LOGIN_PAGE,
        canLoad: [AuthorizationRedirectGuard],
        data: {
          locationCode: BannerLocationCode.SITE_REGISTRATION_LANDING_PAGE
        },
        loadChildren: () => import('@registration/shared/modules/site-registration-login-page/site-registration-login-page.module')
          .then(m => m.SiteRegistrationLoginPageModule)
      },
      {
        path: AdjudicationRoutes.LOGIN_PAGE,
        canLoad: [AuthorizationRedirectGuard],
        loadChildren: () => import('@adjudication/shared/modules/admin-login-page/admin-login-page.module')
          .then(m => m.AdminLoginPageModule)
      },
      {
        path: PhsaEformsRoutes.LOGIN_PAGE,
        canLoad: [AuthorizationRedirectGuard],
        loadChildren: () => import('@phsa/shared/modules/phsa-eforms-login-page/phsa-eforms-login-page.module')
          .then(m => m.PhsaEformsLoginPageModule)
      },
      {
        path: SatEformsRoutes.LOGIN_PAGE,
        loadChildren: () => import('@sat/shared/modules/sat-eforms-login-page/sat-eforms-login-page.module')
          .then(m => m.SatEformsLoginPageModule)
      },
      {
        path: GisEnrolmentRoutes.LOGIN_PAGE,
        canLoad: [GisAuthorizationRedirectGuard],
        loadChildren: () => import('@gis/shared/modules/gis-login/gis-login-page.module')
          .then(m => m.GisLoginPageModule)
      },
      {
        path: HealthAuthSiteRegRoutes.LOGIN_PAGE,
        canLoad: [AuthorizationRedirectGuard],
        loadChildren: () => import('@health-auth/shared/modules/health-auth-site-reg-login-page/health-auth-site-reg-login-page.module')
          .then(m => m.HealthAuthSiteRegLoginPageModule)
      },
      {
        path: '', // Equivalent to `/` and alias for `info`
        redirectTo: EnrolmentRoutes.BCSC_LOGIN,
        pathMatch: 'full'
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuthRoutingModule {}
