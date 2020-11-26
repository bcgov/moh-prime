import { NgModule } from '@angular/core';

import { DashboardModule } from '@lib/modules/dashboard/dashboard.module';

import { SharedModule } from '@shared/shared.module';

import { PhsaLabtechRoutingModule } from './phsa-labtech-routing.module';
import { PhsaLabtechDashboardComponent } from './shared/components/phsa-labtech-dashboard/phsa-labtech-dashboard.component';

import { ExampleComponent } from './pages/example/example.component';
import { AccessCodeComponent } from './pages/access-code/access-code.component';
import { EnrolmentModule } from '@enrolment/enrolment.module';

@NgModule({
  declarations: [
    PhsaLabtechDashboardComponent,
    ExampleComponent,
    AccessCodeComponent
  ],
  imports: [
    SharedModule,
    PhsaLabtechRoutingModule,
    DashboardModule,
    EnrolmentModule
  ]
})
export class PhsaLabtechModule { }
