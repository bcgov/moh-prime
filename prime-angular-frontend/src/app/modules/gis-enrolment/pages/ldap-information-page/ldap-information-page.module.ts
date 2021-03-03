import { NgModule } from '@angular/core';

import { SharedModule } from '@shared/shared.module';

import { LdapInformationPageRoutingModule } from './ldap-information-page-routing.module';
import { LdapInformationPageComponent } from './ldap-information-page.component';


@NgModule({
  declarations: [LdapInformationPageComponent],
  imports: [
    SharedModule,
    LdapInformationPageRoutingModule
  ]
})
export class LdapInformationPageModule { }
