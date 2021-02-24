import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { LdapUserPageComponent } from './ldap-user-page.component';

const routes: Routes = [
  {
    path: '',
    component: LdapUserPageComponent,
    data: { title: 'GIS Enrolment' }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LdapUserPageRoutingModule { }
