import { NgModule } from '@angular/core';

import { SharedModule } from '@shared/shared.module';

import { GisLoginRoutingModule } from './gis-login-routing.module';
import { GisLoginPageComponent } from './gis-login-page.component';

@NgModule({
  declarations: [
    GisLoginPageComponent
  ],
  imports: [
    GisLoginRoutingModule,
    SharedModule
  ]
})
export class GisLoginModule { }
