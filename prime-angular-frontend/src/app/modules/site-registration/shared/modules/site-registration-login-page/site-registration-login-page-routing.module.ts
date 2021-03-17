import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { SiteRegistrationLoginPageComponent } from './site-registration-login-page.component';

const routes: Routes = [
  {
    path: '',
    component: SiteRegistrationLoginPageComponent,
    data: { title: 'Site Registration for PharmaNet Access' }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SiteRegistrationLoginPageRoutingModule { }
