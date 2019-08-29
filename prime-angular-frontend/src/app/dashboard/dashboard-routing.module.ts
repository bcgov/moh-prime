import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { DashboardComponent } from './components/dashboard/dashboard.component';

import { ApplicantsModule } from '../features/applicants/applicants.module';
import { AdminsModule } from '../features/admins/admins.module';

const routes: Routes = [
  {
    path: 'dashboard',
    component: DashboardComponent,
    canActivate: [],
    children: [
      {
        path: 'applicant',
        // Guard module from being accessed without the proper
        // authorization based on the user role permissions, and
        // attempt to redirect admins
        // loadChildren: () => import(`../features/applicants/applicants.module`).then(m => m.ApplicantsModule),
        loadChildren: '../features/applicants/applicants.module#ApplicantsModule'
      },
      {
        path: 'admin',
        // Guard module from being accessed without the proper
        // authorization based on the user role permissions, and
        // attempt to redirect admins
        // loadChildren: () => import(`../features/admins/admins.module`).then(m => m.AdminsModule),
        loadChildren: '../features/admins/admins.module#AdminsModule'
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
