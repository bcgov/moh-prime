import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AppRoutes } from './app.routes';
import { AccessDeniedComponent } from '@common/components/access-denied/access-denied.component';
import { UnsupportedComponent } from '@common/components/unsupported/unsupported.component';
import { MaintenanceComponent } from '@common/components/maintenance/maintenance.component';
import { PageNotFoundComponent } from '@common/components/page-not-found/page-not-found.component';
import { HelpComponent } from '@common/components/help/help.component';

import { AuthRoutes } from '@auth/auth.routes';
import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { ProvisionerAccessRoutes } from '@certificate/provisioner-access.routes';
import { SiteRoutes } from '@registration/site-registration.routes';

const routes: Routes = [
  {
    path: AuthRoutes.MODULE_PATH,
    loadChildren: () => import('./modules/auth/auth.module').then(m => m.AuthModule)
  },
  {
    path: EnrolmentRoutes.MODULE_PATH,
    loadChildren: () => import('./modules/enrolment/enrolment.module').then(m => m.EnrolmentModule)
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
    path: SiteRoutes.MODULE_PATH,
    loadChildren: () => import('./modules/site-registration/site-registration.module').then(m => m.SiteRegistrationModule)
  },
  {
    path: AppRoutes.DENIED,
    component: AccessDeniedComponent,
    data: {
      title: 'Access Denied'
    }
  },
  {
    path: AppRoutes.UNSUPPORTED,
    component: UnsupportedComponent,
    data: {
      title: 'Unsupported Browser'
    }
  },
  {
    path: AppRoutes.MAINTENANCE,
    component: MaintenanceComponent,
    data: {
      title: 'Under Scheduled Maintenace'
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
export class AppRoutingModule { }
