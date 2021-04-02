import { NgModule } from '@angular/core';

import { DashboardModule } from '@lib/modules/dashboard/dashboard.module';
import { SharedModule } from '@shared/shared.module';

import { VaccinationLoginRoutingModule } from './vaccination-login-routing.module';
import { VaccinationLoginComponent } from './vaccination-login.component';

@NgModule({
  declarations: [
    VaccinationLoginComponent
  ],
  imports: [
    SharedModule,
    DashboardModule,
    VaccinationLoginRoutingModule
  ]
})
export class VaccinationLoginModule { }
