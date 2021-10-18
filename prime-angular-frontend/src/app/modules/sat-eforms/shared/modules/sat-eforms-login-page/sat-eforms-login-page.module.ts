import { NgModule } from '@angular/core';

import { DashboardModule } from '@lib/modules/dashboard/dashboard.module';
import { SharedModule } from '@shared/shared.module';

import { SatEformsLoginPageRoutingModule } from './sat-eforms-login-page-routing.module';
import { SatEformsLoginPageComponent } from './sat-eforms-login-page.component';

@NgModule({
  declarations: [
    SatEformsLoginPageComponent
  ],
  imports: [
    SharedModule,
    DashboardModule,
    SatEformsLoginPageRoutingModule
  ]
})
export class SatEformsLoginPageModule { }
