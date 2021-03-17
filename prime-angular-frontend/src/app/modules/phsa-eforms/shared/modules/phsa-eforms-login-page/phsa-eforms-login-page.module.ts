import { NgModule } from '@angular/core';

import { DashboardModule } from '@lib/modules/dashboard/dashboard.module';
import { SharedModule } from '@shared/shared.module';

import { PhsaEformsLoginPageRoutingModule } from './phsa-eforms-login-page-routing.module';
import { PhsaEformsLoginPageComponent } from './phsa-eforms-login-page.component';


@NgModule({
  declarations: [
    PhsaEformsLoginPageComponent
  ],
  imports: [
    SharedModule,
    DashboardModule,
    PhsaEformsLoginPageRoutingModule
  ]
})
export class PhsaEformsLoginPageModule { }
