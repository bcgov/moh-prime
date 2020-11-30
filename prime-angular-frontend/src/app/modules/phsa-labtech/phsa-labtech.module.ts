import { NgModule } from '@angular/core';

import { DashboardModule } from '@lib/modules/dashboard/dashboard.module';

import { SharedModule } from '@shared/shared.module';

import { PhsaLabtechRoutingModule } from './phsa-labtech-routing.module';
import { PhsaLabtechDashboardComponent } from './shared/components/phsa-labtech-dashboard/phsa-labtech-dashboard.component';

import { ExampleComponent } from './pages/example/example.component';
import { BcscDemographicComponent } from './pages/bcsc-demographic/bcsc-demographic.component';

@NgModule({
  declarations: [
    PhsaLabtechDashboardComponent,
    ExampleComponent,
    BcscDemographicComponent
  ],
  imports: [
    SharedModule,
    PhsaLabtechRoutingModule,
    DashboardModule
  ]
})
export class PhsaLabtechModule { }
