import { NgModule } from '@angular/core';

import { SharedModule } from '@shared/shared.module';

import { AdminsRoutingModule } from '@admins/admins-routing.module';
import { ApplicantsComponent } from '@admins/pages/applicants/applicants.component';

@NgModule({
  declarations: [
    // TODO: add admin components
    ApplicantsComponent
  ],
  imports: [
    SharedModule,
    AdminsRoutingModule
  ]
})
export class AdminsModule { }
