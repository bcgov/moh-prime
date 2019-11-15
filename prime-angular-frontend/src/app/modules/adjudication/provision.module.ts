import { NgModule } from '@angular/core';

import { SharedModule } from '@shared/shared.module';
import { ProvisionRoutingModule } from './provision-routing.module';

import { EnrolmentsComponent } from './pages/enrolments/enrolments.component';
import { EnrolmentComponent } from './pages/enrolment/enrolment.component';

@NgModule({
  declarations: [
    EnrolmentsComponent,
    EnrolmentComponent
  ],
  imports: [
    SharedModule,
    ProvisionRoutingModule
  ]
})
export class ProvisionModule { }
