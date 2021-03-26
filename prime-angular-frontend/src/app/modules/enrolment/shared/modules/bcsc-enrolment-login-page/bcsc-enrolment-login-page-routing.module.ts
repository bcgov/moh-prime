import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { BcscEnrolmentLoginPageComponent } from './bcsc-enrolment-login-page.component';

const routes: Routes = [
  {
    path: '',
    component: BcscEnrolmentLoginPageComponent,
    data: { title: 'Welcome to PRIME' }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class BcscEnrolmentLoginPageRoutingModule { }
