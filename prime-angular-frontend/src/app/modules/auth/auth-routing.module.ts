import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AuthComponent } from './shared/components/auth/auth.component';
import { AuthorizationRedirectGuard } from './shared/guards/authorization-redirect.guard';

import { AuthRoutes } from './auth.routes';

import { InfoComponent } from './pages/info/info.component';
import { BceidComponent } from './pages/bceid/bceid.component';
import { AdminComponent } from './pages/admin/admin.component';
import { SiteComponent } from './pages/site/site.component';
import { PhsaComponent } from './pages/phsa/phsa.component';
import { GisEnrolmentRoutes } from '@gis/gis-enrolment.routes';

const routes: Routes = [
  {
    path: AuthRoutes.MODULE_PATH,
    component: AuthComponent,
    canActivate: [
      AuthorizationRedirectGuard
    ],
    children: [
      {
        path: AuthRoutes.INFO,
        component: InfoComponent,
        data: { title: 'Welcome to PRIME' }
      },
      {
        path: AuthRoutes.BCEID,
        component: BceidComponent,
        data: { title: 'Welcome to PRIME' }
      },
      {
        path: AuthRoutes.ADMIN,
        component: AdminComponent,
        data: { title: 'Welcome to PRIME' }
      },
      {
        path: AuthRoutes.SITE,
        component: SiteComponent,
        data: { title: 'Welcome to PRIME' }
      },
      {
        path: AuthRoutes.PHSA,
        component: PhsaComponent,
        data: { title: 'Enrol for access to PHSA eForms' }
      },
      {
        path: '', // Equivalent to `/` and alias for `info`
        redirectTo: AuthRoutes.INFO,
        pathMatch: 'full'
      }
    ]
  },
  {
    path: GisEnrolmentRoutes.LOGIN_PAGE,
    loadChildren: () => import('../gis-enrolment/shared/modules/gis-login/gis-login.module').then(m => m.GisLoginModule)
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuthRoutingModule { }
