import { NgModule } from '@angular/core';

import { SharedModule } from 'src/app/shared/shared.module';

import { AdminsRoutingModule } from './admins-routing.module';
import { ApplicantsComponent, DialogDeleteEnrolment } from './pages/applicants/applicants.component';

@NgModule({
  declarations: [
    ApplicantsComponent,
    DialogDeleteEnrolment
  ],
  imports: [
    SharedModule,
    AdminsRoutingModule
  ],
  entryComponents: [
    DialogDeleteEnrolment
  ]
})
export class AdminsModule { }
