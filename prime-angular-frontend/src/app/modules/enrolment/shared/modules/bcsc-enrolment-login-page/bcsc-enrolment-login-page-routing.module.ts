import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { BannerLocationCode } from '@shared/enums/banner-location-code.enum';

import { BcscEnrolmentLoginPageComponent } from './bcsc-enrolment-login-page.component';

const routes: Routes = [
  {
    path: '',
    component: BcscEnrolmentLoginPageComponent,
    data: {
      title: 'Welcome to PRIME',
      locationCode: BannerLocationCode.ENROLMENT_LANDING_PAGE
    }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class BcscEnrolmentLoginPageRoutingModule { }
