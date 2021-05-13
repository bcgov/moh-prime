import { NgModule } from '@angular/core';

import { DashboardModule } from '@lib/modules/dashboard/dashboard.module';
import { SharedModule } from '@shared/shared.module';

import { GisLoginRoutingModule } from './gis-login-routing.module';
import { GisLoginPageComponent } from './gis-login-page.component';

@NgModule({
  declarations: [
    GisLoginPageComponent
  ],
  imports: [
    SharedModule,
    DashboardModule,
    GisLoginRoutingModule
  ]
})
export class GisLoginPageModule { }
