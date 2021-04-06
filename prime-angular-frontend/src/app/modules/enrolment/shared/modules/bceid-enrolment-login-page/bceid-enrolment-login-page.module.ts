import { NgModule } from '@angular/core';

import { DashboardModule } from '@lib/modules/dashboard/dashboard.module';
import { SharedModule } from '@shared/shared.module';

import { BceidEnrolmentLoginPageRoutingModule } from './bceid-enrolment-login-page-routing.module';
import { BceidEnrolmentLoginPageComponent } from './bceid-enrolment-login-page.component';

@NgModule({
  declarations: [
    BceidEnrolmentLoginPageComponent
  ],
  imports: [
    SharedModule,
    DashboardModule,
    BceidEnrolmentLoginPageRoutingModule
  ]
})
export class BceidEnrolmentLoginPageModule { }
