import { NgModule } from '@angular/core';

import { SharedModule } from '@shared/shared.module';

import { OrganizationInformationPageRoutingModule } from './organization-information-page-routing.module';
import { OrganizationInformationPageComponent } from './organization-information-page.component';

@NgModule({
  declarations: [OrganizationInformationPageComponent],
  imports: [
    SharedModule,
    OrganizationInformationPageRoutingModule
  ]
})
export class OrganizationInformationPageModule { }
