import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { GisLoginPageComponent } from './gis-login-page.component';

const routes: Routes = [
  {
    path: '',
    component: GisLoginPageComponent,
    data: { title: 'GIS Enrolment' }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class GisLoginRoutingModule { }
