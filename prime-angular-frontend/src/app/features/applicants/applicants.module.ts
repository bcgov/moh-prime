import { NgModule } from '@angular/core';

import { SharedModule } from '@shared/shared.module';

import { ApplicantsRoutingModule } from '@applicants/applicants-routing.module';
import { EnrolmentComponent } from './pages/enrolment/enrolment.component';

@NgModule({
  declarations: [
    EnrolmentComponent
  ],
  imports: [
    SharedModule,
    ApplicantsRoutingModule
  ]
})
export class ApplicantsModule { }
