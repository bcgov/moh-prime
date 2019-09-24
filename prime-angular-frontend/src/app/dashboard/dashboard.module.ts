import { NgModule } from '@angular/core';

import { SharedModule } from '@shared/shared.module';

import { DashboardRoutingModule } from '@dashboard/dashboard-routing.module';
import { DashboardComponent } from '@dashboard/shared/components/dashboard/dashboard.component';

@NgModule({
  declarations: [
    DashboardComponent
  ],
  imports: [
    SharedModule,
    DashboardRoutingModule
  ]
})
export class DashboardModule { }
