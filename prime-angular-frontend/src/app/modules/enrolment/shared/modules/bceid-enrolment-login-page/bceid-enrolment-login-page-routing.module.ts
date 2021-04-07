import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { BceidEnrolmentLoginPageComponent } from './bceid-enrolment-login-page.component';

const routes: Routes = [
  {
    path: '',
    component: BceidEnrolmentLoginPageComponent,
    data: { title: 'Welcome to PRIME' }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class BceidEnrolmentLoginPageRoutingModule { }
