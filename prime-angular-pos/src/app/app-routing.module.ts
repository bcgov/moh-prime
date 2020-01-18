import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AppRoutes } from './app.routes';
import { AccessDeniedComponent } from '@core/components/access-denied/access-denied.component';
import { MaintenanceComponent } from '@core/components/maintenance/maintenance.component';
import { PageNotFoundComponent } from '@core/components/page-not-found/page-not-found.component';
import { AuthRoutes } from './modules/auth/auth.routes';

const routes: Routes = [
  {
    path: AuthRoutes.MODULE_PATH,
    loadChildren: () => import('./modules/auth/auth.module').then(mod => mod.AuthModule)
  },
  {
    path: AppRoutes.DENIED,
    component: AccessDeniedComponent,
    data: {
      title: 'Access Denied'
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
    path: AppRoutes.PAGE_NOT_FOUND,
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
