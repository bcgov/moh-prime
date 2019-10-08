import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AuthGuard } from '@auth/shared/guards/auth.guard';
import { AdminGuard } from '@admins/shared/guards/admin.guard';

// TODO: add redirect for admins, but has circlar dependency
import { RedirectGuard } from '@dashboard/shared/guards/redirect.guard';
import { DashboardComponent } from '@dashboard/shared/components/dashboard/dashboard.component';

const routes: Routes = [
  {
    path: 'dashboard',
    component: DashboardComponent,
    // canActivate: [AuthGuard],
    children: [
      // {
      //   path: 'enrolment',
      //   // Guard modules from being accessed without the proper
      //   // authorization based on the user role permissions, and
      //   // attempt to redirect admins
      //   canActivate: [RedirectGuard, EnrolmentGuard],
      //   // TODO: issue with new import syntax when built for production
      //   // loadChildren: () => import(`../features/enrolments/enrolments.module`).then(m => m.EnrolmentsModule),
      //   loadChildren: '../features/enrolments/enrolments.module#EnrolmentsModule'
      // },
      {
        path: 'admin',
        // Guard module from being accessed without the proper
        // authorization based on the user role permissions
        canLoad: [AdminGuard],
        // TODO: issue with new import syntax when built for production
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
