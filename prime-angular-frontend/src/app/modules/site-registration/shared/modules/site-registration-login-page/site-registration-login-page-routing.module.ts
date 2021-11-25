import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { BannerLocationCode } from '@shared/enums/banner-location-code.enum';

import { CommunitySiteRegLoginRoutes } from './site-registration-login.routes';
import { SiteRegistrationLoginPageComponent } from './site-registration-login-page.component';

const routes: Routes = [
  {
    path: CommunitySiteRegLoginRoutes.SITE_DEFAULT_LOGIN,
    component: SiteRegistrationLoginPageComponent,
    data: {
      title: 'Site Registration for PharmaNet Access',
      locationCode: BannerLocationCode.SITE_REGISTRATION_LANDING_PAGE,
      redirectRedirectSegment: CommunitySiteRegLoginRoutes.SITE_DEFAULT_LOGIN
    }
  },
  {
    path: CommunitySiteRegLoginRoutes.SITE_CHANGE_LOGIN,
    component: SiteRegistrationLoginPageComponent,
    data: {
      title: 'Site Registration for PharmaNet Access',
      locationCode: BannerLocationCode.SITE_REGISTRATION_LANDING_PAGE,
      redirectRedirectSegment: CommunitySiteRegLoginRoutes.SITE_CHANGE_LOGIN
    }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SiteRegistrationLoginPageRoutingModule { }
