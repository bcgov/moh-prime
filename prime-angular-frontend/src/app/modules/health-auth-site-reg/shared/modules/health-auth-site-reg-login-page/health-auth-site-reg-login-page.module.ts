import { NgModule } from '@angular/core';

import { DashboardModule } from '@lib/modules/dashboard/dashboard.module';
import { SharedModule } from '@shared/shared.module';

import { HealthAuthSiteRegLoginPageRoutingModule } from './health-auth-site-reg-login-page-routing.module';
import { HealthAuthSiteRegLoginPageComponent } from './health-auth-site-reg-login-page.component';

@NgModule({
  declarations: [
    HealthAuthSiteRegLoginPageComponent
  ],
  imports: [
    SharedModule,
    DashboardModule,
    HealthAuthSiteRegLoginPageRoutingModule
  ]
})
export class HealthAuthSiteRegLoginPageModule { }
