import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AuthComponent } from './shared/components/auth/auth.component';
import { AuthRedirectGuard } from './shared/guards/auth-redirect.guard';

import { InfoComponent } from './pages/info/info.component';
import { LoginComponent } from './pages/login/login.component';

const routes: Routes = [
  {
    path: '',
    component: AuthComponent,
    canActivate: [AuthRedirectGuard],
    children: [
      {
        path: 'info',
        component: InfoComponent,
        data: { title: 'Welcome - PRIME' }
      },
      {
        path: 'login',
        component: LoginComponent,
        data: { title: 'Sign In - PRIME' }
      },
      {
        path: '', // Equivalent to `/` and alias for `login`
        redirectTo: 'info',
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
