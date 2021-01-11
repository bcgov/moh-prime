import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AppRoutes } from './app.routes';
import { AccessDeniedComponent } from '@lib/modules/root-routes/components/access-denied/access-denied.component';
import { MaintenanceComponent } from '@lib/modules/root-routes/components/maintenance/maintenance.component';
import { PageNotFoundComponent } from '@lib/modules/root-routes/components/page-not-found/page-not-found.component';
import { HelpComponent } from '@lib/modules/root-routes/components/help/help.component';
import { UnderagedComponent } from '@lib/modules/root-routes/components/underaged/underaged.component';

import { ProvisionerAccessRoutes } from '@certificate/provisioner-access.routes';

const routes: Routes = [
  {
    path: ProvisionerAccessRoutes.MODULE_PATH,
    loadChildren: () => import('./modules/provisioner-access/provisioner-access.module').then(m => m.ProvisionerAccessModule)
  },
  // {
  //   path: PhsaEformsRoutes.MODULE_PATH,
  //   loadChildren: () => import('./modules/phsa-eforms/phsa-eforms.module').then(m => m.PhsaEformsModule)
  // },
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
