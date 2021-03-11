import { NgModule } from '@angular/core';

import { SharedModule } from '@shared/shared.module';

import { EnrolleeInformationPageRoutingModule } from './enrollee-information-page-routing.module';
import { EnrolleeInformationPageComponent } from './enrollee-information-page.component';


@NgModule({
  declarations: [EnrolleeInformationPageComponent],
  imports: [
    SharedModule,
    EnrolleeInformationPageRoutingModule
  ]
})
export class EnrolleeInformationPageModule { }
