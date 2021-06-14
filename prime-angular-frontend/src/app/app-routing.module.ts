import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AppRoutes } from './app.routes';
import { AccessDeniedComponent } from '@lib/modules/root-routes/components/access-denied/access-denied.component';
import { MaintenanceComponent } from '@lib/modules/root-routes/components/maintenance/maintenance.component';
import { PageNotFoundComponent } from '@lib/modules/root-routes/components/page-not-found/page-not-found.component';
import { HelpComponent } from '@lib/modules/root-routes/components/help/help.component';
import { UnderagedComponent } from '@lib/modules/root-routes/components/underaged/underaged.component';

import { AuthRoutes } from '@auth/auth.routes';
import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { ProvisionerAccessRoutes } from '@certificate/provisioner-access.routes';
import { PhsaEformsRoutes } from '@phsa/phsa-eforms.routes';
import { GisEnrolmentRoutes } from '@gis/gis-enrolment.routes';
import { HealthAuthSiteRegRoutes } from '@health-auth/health-auth-site-reg.routes';

const routes: Routes = [
  {
    path: AuthRoutes.MODULE_PATH,
    loadChildren: () => import('./modules/auth/auth.module').then(m => m.AuthModule)
  },
  {
    path: AdjudicationRoutes.MODULE_PATH,
    loadChildren: () => import('./modules/adjudication/adjudication.module').then(m => m.AdjudicationModule)
  },
  {
    path: ProvisionerAccessRoutes.MODULE_PATH,
    loadChildren: () => import('./modules/provisioner-access/provisioner-access.module').then(m => m.ProvisionerAccessModule)
  },
  {
    path: PhsaEformsRoutes.MODULE_PATH,
    loadChildren: () => import('./modules/phsa-eforms/phsa-eforms.module').then(m => m.PhsaEformsModule)
  },
  {
    path: GisEnrolmentRoutes.MODULE_PATH,
    loadChildren: () => import('./modules/gis-enrolment/gis-enrolment.module').then(m => m.GisEnrolmentModule)
  },
  {
    path: HealthAuthSiteRegRoutes.MODULE_PATH,
    loadChildren: () => import('./modules/health-auth-site-reg/health-auth-site-reg.module').then(m => m.HealthAuthSiteRegModule)
  },
  {
    path: AppRoutes.DENIED,
    component: AccessDeniedComponent,
    data: {
      title: 'Access Denied'
    }
  },
  {
    path: AppRoutes.UNDERAGED,
    component: UnderagedComponent,
    data: {
      title: 'Underaged'
    }
  },
  {
    path: AppRoutes.MAINTENANCE,
    component: MaintenanceComponent,
    data: {
      title: 'Under Scheduled Maintenance'
    }
  },
  {
    // Allow for direct routing to page not found
    path: AppRoutes.PAGE_NOT_FOUND,
    component: PageNotFoundComponent,
    data: {
      title: 'Page Not Found'
    }
  },
  {
    path: AppRoutes.HELP,
    component: HelpComponent,
    data: {
      title: 'Help'
    }
  },
  {
    path: AppRoutes.DEFAULT,
    component: PageNotFoundComponent,
    data: {
      title: 'Page Not Found'
    }
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { enableTracing: false })],
  exports: [RouterModule]
})
export class AppRoutingModule {}
