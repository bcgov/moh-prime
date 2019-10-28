import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { DashboardComponent } from '@shared/components/dashboard/dashboard.component';
import { AuthenticateGuard } from '@auth/shared/guards/authentication.guard';

import { ProvisionGuard } from './shared/guards/provision.guard';

import { EnrolmentsComponent } from './pages/enrolments/enrolments.component';
import { EnrolmentComponent } from './pages/enrolment/enrolment.component';

const routes: Routes = [
  {
    path: 'provision',
    component: DashboardComponent,
    // Check authentication and authorization each time
    // the router navigates to the next route
    canActivateChild: [
      AuthenticateGuard,
      // Guard module from being accessed without the proper
      // authorization based on the user role permissions
      ProvisionGuard
    ],
    children: [
      {
        path: 'enrolments',
        children: [
          {
            path: '',
            component: EnrolmentsComponent,
            canDeactivate: []
          },
          {
            path: ':id',
            component: EnrolmentComponent,
            canDeactivate: []
          }
        ]
      },
      {
        path: '', // Equivalent to `/` and alias for `enrolments`
        redirectTo: 'enrolments',
        pathMatch: 'full'
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProvisionRoutingModule { }
