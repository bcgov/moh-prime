import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ApplicantsComponent } from './pages/applicants/applicants.component';

const routes: Routes = [
  {
    path: '',
    // Check authentication and authorization each time
    // the router navigates to the next route
    // TODO: add admin route guard
    canActivateChild: [],
    children: [
      {
        path: 'applicants',
        component: ApplicantsComponent,
        data: { title: 'Applicants - PRIME' }
      },
      {
        path: '', // Equivalent to `/` and alias for `applicants`
        redirectTo: 'applicants',
        pathMatch: 'full'
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminsRoutingModule { }
