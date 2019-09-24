import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AuthComponent } from '@auth/shared/components/auth/auth.component';
import { AuthRedirectGuard } from '@auth/shared/guards/auth-redirect.guard';
import { LoginComponent } from '@auth/pages/login/login.component';

const routes: Routes = [
  {
    path: '',
    component: AuthComponent,
    canActivate: [AuthRedirectGuard],
    children: [
      {
        path: 'login',
        component: LoginComponent,
        data: { title: 'Sign In - PRIME' }
      },
      {
        path: '', // Equivalent to `/` and alias for `login`
        redirectTo: 'login',
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
