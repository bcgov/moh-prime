import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { DashboardComponent } from './components/dashboard/dashboard.component';

const routes: Routes = [
  {
    path: 'dashboard',
    component: DashboardComponent,
    canActivate: [],
    children: [
      {
        path: 'applicant',
        // TODO: Guard module from being accessed without the proper
        // authorization based on the user role permissions, and
        // attempt to redirect admins
        loadChildren: () => import(`../features/applicants/applicants.module`).then(m => m.ApplicantsModule),
      },
      {
        path: 'admin',
        // TODO: Guard module from being accessed without the proper
        // authorization based on the user role permissions, and
        // attempt to redirect admins
        loadChildren: () => import(`../features/admins/admins.module`).then(m => m.AdminsModule),
      },
      {
        path: '', // Equivalent to `/` and alias for `applicant`
        redirectTo: 'applicant',
        pathMatch: 'full'
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DashboardRoutingModule { }
