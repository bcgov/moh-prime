import { NgModule } from '@angular/core';

import { DashboardModule } from '@lib/modules/dashboard/dashboard.module';

import { SharedModule } from '@shared/shared.module';

import { PhsaLabtechRoutingModule } from './phsa-labtech-routing.module';
import { PhsaLabtechDashboardComponent } from './shared/components/phsa-labtech-dashboard/phsa-labtech-dashboard.component';

import { AccessCodeComponent } from './pages/access-code/access-code.component';
import { PhsaProgressIndicatorComponent } from './shared/components/phsa-progress-indicator/phsa-progress-indicator.component';

@NgModule({
  declarations: [
    PhsaLabtechDashboardComponent,
    AccessCodeComponent,
    PhsaProgressIndicatorComponent
  ],
  imports: [
    SharedModule,
    PhsaLabtechRoutingModule,
    DashboardModule
  ]
})
export class PhsaLabtechModule { }
