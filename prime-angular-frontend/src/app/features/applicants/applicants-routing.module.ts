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
        path: 'enrolment',
        component: EnrollmentComponent,
        data: { title: 'Optimize PRIME - Auto-bot, enroll out!' }
      },
      {
        path: 'inprogress',
        component: AuthenticateInProgressComponent,
        data: { title: 'Optimize PRIME - Enrolment In Progress' }
      },
      {
        path: 'complete',
        component: AuthenticateCompleteComponent,
        data: { title: 'Optimize PRIME - Enrolment Complete' }
      },
      {
        path: 'denied',
        component: AuthenticateDeniedComponent,
        data: { title: 'Optimize PRIME - Enrolment Denied' }
      },
      {
        path: '', // Equivalent to `/` and alias for `overview`
        redirectTo: 'enrolment',
        pathMatch: 'full'
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ApplicantsRoutingModule { }
