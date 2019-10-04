import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { EnrolmentComponent } from './pages/enrolment/enrolment.component';

const routes: Routes = [
  {
    path: '',
    // Check authentication and authorization each time
    // the router navigates to the next route
    // TODO: add applicant route guard
    // canActivateChild: [],
    children: [
      {
        path: 'enrolment',
        component: EnrolmentComponent,
        data: { title: 'Enrolment - PRIME' }
      },
      {
        path: '', // Equivalent to `/` and alias for `enrolment`
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
