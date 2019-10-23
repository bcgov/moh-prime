import { NgModule } from '@angular/core';

import { SharedModule } from '@shared/shared.module';
import { ProvisionRoutingModule } from './provision-routing.module';

import { ApplicationsComponent } from './pages/applications/applications.component';
import { ApplicationComponent } from './pages/application/application.component';

@NgModule({
  declarations: [
    ApplicationsComponent,
    ApplicationComponent
  ],
  imports: [
    SharedModule,
    ProvisionRoutingModule
  ]
})
export class ProvisionModule { }
