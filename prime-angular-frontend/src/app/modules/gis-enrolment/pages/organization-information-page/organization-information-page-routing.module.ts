import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { OrganizationInformationPageComponent } from './organization-information-page.component';

export const route: string = 'org-info';

const routes: Routes = [
  {
    path: '',
    component: OrganizationInformationPageComponent,
    data: { title: 'GIS Enrolment' }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class OrganizationInformationPageRoutingModule { }
