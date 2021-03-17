import { NgModule } from '@angular/core';

import { DashboardModule } from '@lib/modules/dashboard/dashboard.module';
import { SharedModule } from '@shared/shared.module';

import { SiteRegistrationLoginPageRoutingModule } from './site-registration-login-page-routing.module';
import { SiteRegistrationLoginPageComponent } from './site-registration-login-page.component';

@NgModule({
  declarations: [
    SiteRegistrationLoginPageComponent
  ],
  imports: [
    SharedModule,
    DashboardModule,
    SiteRegistrationLoginPageRoutingModule
  ]
})
export class SiteRegistrationLoginPageModule { }
