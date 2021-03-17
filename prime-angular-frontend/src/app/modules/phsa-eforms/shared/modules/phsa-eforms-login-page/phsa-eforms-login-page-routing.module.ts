import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { PhsaEformsLoginPageComponent } from './phsa-eforms-login-page.component';

const routes: Routes = [
  {
    path: '',
    component: PhsaEformsLoginPageComponent,
    data: { title: 'Enrol for access to PHSA eForms' }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PhsaEformsLoginPageRoutingModule { }
