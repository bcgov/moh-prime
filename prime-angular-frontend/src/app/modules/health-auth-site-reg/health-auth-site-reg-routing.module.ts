import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { DashboardComponent } from '@lib/modules/dashboard/components/dashboard/dashboard.component';
import { HealthAuthSiteRegRoutes } from './health-auth-site-reg.routes';
import { HealthAuthSiteRegGuard } from './shared/guards/health-auth-site-reg.guard';
import { AuthorizedUserComponent } from './pages/authorized-user/authorized-user.component';

const routes: Routes = [
  {
    path: '',
    component: DashboardComponent,
    canLoad: [
      HealthAuthSiteRegGuard
    ],
    canActivate: [],
    canActivateChild: [
      HealthAuthSiteRegGuard
    ],
    children: [
      {
        path: HealthAuthSiteRegRoutes.AUTHORIZED_USER,
        component: AuthorizedUserComponent,
        data: { title: '' }
      },
      {
        path: '', // Equivalent to `/` and alias for default view
        // TODO change to site management and hook up guards, but for demo use AuthorizedUser
        // redirectTo: HealthAuthSiteRegRoutes.SITE_MANAGEMENT,
        redirectTo: HealthAuthSiteRegRoutes.AUTHORIZED_USER,
        pathMatch: 'full'
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HealthAuthSiteRegRoutingModule { }
