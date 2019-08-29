import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { EnrollmentComponent } from './pages/enrollment/enrollment.component';
import { AuthenticateInProgressComponent } from './pages/authenticate-in-progress/authenticate-in-progress.component';
import { AuthenticateCompleteComponent } from './pages/authenticate-complete/authenticate-complete.component';
import { AuthenticateDeniedComponent } from './pages/authenticate-denied/authenticate-denied.component';

const routes: Routes = [
  {
    path: '',
    // Check authentication and authorization each time
    // the router navigates to the next route
    canActivateChild: [],
    children: [
      {
        path: 'enrollment',
        component: EnrollmentComponent,
        data: { title: 'Enrollment' }
      },
      {
        path: 'inprogress',
        component: AuthenticateInProgressComponent,
        data: { title: 'Authentication In Progress' }
      },
      {
        path: 'complete',
        component: AuthenticateCompleteComponent,
        data: { title: 'Authentication Complete' }
      },
      {
        path: 'denied',
        component: AuthenticateDeniedComponent,
        data: { title: 'Authentication Denied' }
      },
      {
        path: '', // Equivalent to `/` and alias for `overview`
        redirectTo: 'enrollment',
        pathMatch: 'full',
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ApplicantsRoutingModule { }
