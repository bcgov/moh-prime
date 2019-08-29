import { NgModule } from '@angular/core';

import { SharedModule } from 'src/app/shared/shared.module';

import { AdminsRoutingModule } from './admins-routing.module';

@NgModule({
  declarations: [],
  imports: [
    SharedModule,
    AdminsRoutingModule
  ]
})
export class AdminsModule { }
