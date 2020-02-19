import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { UnsupportedGuard } from '@core/guards/unsupported.guard';
import { AuthComponent } from './shared/components/auth/auth.component';
import { AuthorizationRedirectGuard } from './shared/guards/authorization-redirect.guard';

import { AuthRoutes } from './auth.routes';

import { InfoComponent } from './pages/info/info.component';
import { AdminComponent } from './pages/admin/admin.component';

const routes: Routes = [
  {
    path: AuthRoutes.MODULE_PATH,
    component: AuthComponent,
    canActivate: [
      UnsupportedGuard,
      AuthorizationRedirectGuard
    ],
    children: [
      {
        path: AuthRoutes.INFO,
        component: InfoComponent,
        data: { title: 'Welcome to PRIME' }
      },
      {
        path: AuthRoutes.ADMIN,
        component: AdminComponent,
        data: { title: 'Welcome to PRIME' }
      },
      {
        path: '', // Equivalent to `/` and alias for `info`
        redirectTo: AuthRoutes.INFO,
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
