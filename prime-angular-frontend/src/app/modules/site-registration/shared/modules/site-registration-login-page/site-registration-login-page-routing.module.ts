import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { BannerLocationCode } from '@shared/enums/banner-location-code.enum';

import { SiteRegistrationLoginPageComponent } from './site-registration-login-page.component';

const routes: Routes = [
  {
    path: '',
    component: SiteRegistrationLoginPageComponent,
    data: {
      title: 'Site Registration for PharmaNet Access',
      locationCode: BannerLocationCode.SITE_REGISTRATION_LANDING_PAGE
    }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SiteRegistrationLoginPageRoutingModule { }
