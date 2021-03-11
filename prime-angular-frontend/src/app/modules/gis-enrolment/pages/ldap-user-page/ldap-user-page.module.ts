import { NgModule } from '@angular/core';

import { SharedModule } from '@shared/shared.module';

import { LdapUserPageRoutingModule } from './ldap-user-page-routing.module';
import { LdapUserPageComponent } from './ldap-user-page.component';

@NgModule({
  declarations: [LdapUserPageComponent],
  imports: [
    SharedModule,
    LdapUserPageRoutingModule
  ]
})
export class LdapUserPageModule { }
