import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { EnrolleeInformationPageComponent } from './enrollee-information-page.component';

const routes: Routes = [
  {
    path: '',
    component: EnrolleeInformationPageComponent,
    data: { title: 'GIS Enrolment' }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EnrolleeInformationPageRoutingModule { }
