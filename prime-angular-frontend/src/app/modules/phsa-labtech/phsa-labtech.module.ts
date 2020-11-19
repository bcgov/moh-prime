import { NgModule } from '@angular/core';

import { DashboardModule } from '@lib/modules/dashboard/dashboard.module';

import { SharedModule } from '@shared/shared.module';

import { PhsaLabtechRoutingModule } from './phsa-labtech-routing.module';
import { PhsaLabtechDashboardComponent } from './shared/components/phsa-labtech-dashboard/phsa-labtech-dashboard.component';

import { ExampleComponent } from './pages/example/example.component';

@NgModule({
  declarations: [
    PhsaLabtechDashboardComponent,
    ExampleComponent
  ],
  imports: [
    SharedModule,
    PhsaLabtechRoutingModule,
    DashboardModule
  ]
})
export class PhsaLabtechModule { }
