import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { LdapInformationPageComponent } from './ldap-information-page.component';

const routes: Routes = [
  {
    path: '',
    component: LdapInformationPageComponent,
    data: { title: 'GIS Enrolment' }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LdapInformationPageRoutingModule { }
