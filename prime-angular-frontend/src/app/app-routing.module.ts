import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AccessDeniedComponent } from '@core/components/access-denied/access-denied.component';
import { MaintenanceComponent } from '@core/components/maintenance/maintenance.component';
import { PageNotFoundComponent } from '@core/components/page-not-found/page-not-found.component';

// Provides the redirect route to the dashboard, which is the default
// entry point for users, otherwise the application-level routing
// should not be used for features
const routes: Routes = [
  // TODO: need more requirements to determine default route
  // {
  //   path: '',
  //   redirectTo: '',
  //   pathMatch: 'full',
  // },
  {
    path: 'denied',
    component: AccessDeniedComponent,
    data: {
      title: 'Access Denied'
    }
  },
  {
    path: 'maintenance',
    component: MaintenanceComponent,
    data: {
      title: 'Under Scheduled Maintenace'
    }
  },
  {
    path: '**',
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
