import { NgModule } from '@angular/core';

import { SharedModule } from 'src/app/shared/shared.module';

import { AdminsRoutingModule } from './admins-routing.module';
import { ApplicantsComponent } from './pages/applicants/applicants.component';

@NgModule({
  declarations: [
    ApplicantsComponent
  ],
  imports: [
    SharedModule,
    AdminsRoutingModule
  ]
})
export class AdminsModule { }
