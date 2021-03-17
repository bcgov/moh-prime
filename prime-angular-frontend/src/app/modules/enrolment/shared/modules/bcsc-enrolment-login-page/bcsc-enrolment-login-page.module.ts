import { NgModule } from '@angular/core';

import { DashboardModule } from '@lib/modules/dashboard/dashboard.module';
import { SharedModule } from '@shared/shared.module';

import { BcscEnrolmentLoginPageRoutingModule } from './bcsc-enrolment-login-page-routing.module';
import { BcscEnrolmentLoginPageComponent } from './bcsc-enrolment-login-page.component';

@NgModule({
  declarations: [
    BcscEnrolmentLoginPageComponent
  ],
  imports: [
    SharedModule,
    DashboardModule,
    BcscEnrolmentLoginPageRoutingModule
  ]
})
export class BcscEnrolmentLoginPageModule { }
