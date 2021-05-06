import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { HealthAuthSiteRegLoginPageComponent } from './health-auth-site-reg-login-page.component';

const routes: Routes = [
  {
    path: '',
    component: HealthAuthSiteRegLoginPageComponent,
    data: { title: 'Site Registration for PharmaNet Access' }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HealthAuthSiteRegLoginPageRoutingModule { }
