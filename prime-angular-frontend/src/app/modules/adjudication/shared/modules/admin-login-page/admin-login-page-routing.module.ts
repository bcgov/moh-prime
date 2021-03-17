import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AdminLoginPageComponent } from './admin-login-page.component';

const routes: Routes = [
  {
    path: '',
    component: AdminLoginPageComponent,
    data: { title: 'PRIME Administration' }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminLoginPageRoutingModule { }
