import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ConfigResolver } from '@config/config-resolver';
import { DashboardComponent } from '@shared/components/dashboard/dashboard.component';
import { AuthenticationGuard } from '@auth/shared/guards/authentication.guard';

import { ProvisionGuard } from './shared/guards/provision.guard';

import { EnrolmentsComponent } from './pages/enrolments/enrolments.component';
import { EnrolmentComponent } from './pages/enrolment/enrolment.component';

const routes: Routes = [
  {
    path: 'provision',
    component: DashboardComponent,
    canActivateChild: [
      AuthenticationGuard,
      ProvisionGuard
    ],
    resolve: [ConfigResolver],
    children: [
      {
        path: 'enrolments',
        children: [
          {
            path: '',
            component: EnrolmentsComponent
          },
          {
            path: ':id',
            component: EnrolmentComponent
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
