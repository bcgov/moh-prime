import { NgModule } from '@angular/core';

import { SharedModule } from 'src/app/shared/shared.module';

import { AdminsRoutingModule } from './admins-routing.module';
import { ApplicantsComponent, DialogDeleteEnrolmentComponent } from './pages/applicants/applicants.component';

@NgModule({
  declarations: [
    ApplicantsComponent,
    DialogDeleteEnrolmentComponent
  ],
  imports: [
    SharedModule,
    AdminsRoutingModule
  ],
  entryComponents: [
    DialogDeleteEnrolmentComponent
  ]
})
export class AdminsModule { }
