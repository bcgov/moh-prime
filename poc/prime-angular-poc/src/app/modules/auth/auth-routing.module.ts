import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AuthRoutes } from './auth.routes';
import { LoginComponent } from './pages/login/login.component';

const routes: Routes = [
  {
    path: AuthRoutes.MODULE_PATH,
    canActivate: [],
    children: [
      {
        path: AuthRoutes.LOGIN,
        component: LoginComponent,
        data: { title: 'Login' }
      },
      {
        path: '', // Equivalent to `/` and alias for `login`
        redirectTo: AuthRoutes.LOGIN,
        pathMatch: 'full'
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuthRoutingModule { }
