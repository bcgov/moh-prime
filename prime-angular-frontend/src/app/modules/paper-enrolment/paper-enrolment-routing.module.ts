import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { PaperEnrolmentRoutes } from '@paper-enrolment/paper-enrolment.routes';
import { DemographicComponent } from '@paper-enrolment/pages/demographic/demographic.component';
import { CanDeactivateFormGuard } from '@core/guards/can-deactivate-form.guard';
import { PaperEnrolmentDashboardComponent } from './shared/components/paper-enrolment-dashboard/paper-enrolment-dashboard.component';
import { PaperEnrolmentGuard } from './shared/guards/paper-enrolment.guard';

const routes: Routes = [
  {
    path: '',
    component: PaperEnrolmentDashboardComponent,
    children: [
      {
        // During initial registration the ID will be set to
        // zero indicating the organization does not exist
        path: ':eid',
        canActivateChild: [PaperEnrolmentGuard],
        children: [
          {
            path: PaperEnrolmentRoutes.DEMOGRAPHIC,
            component: DemographicComponent,
            canDeactivate: [CanDeactivateFormGuard],
            data: { title: 'PRIME Enrolment' }
          },
          {
            path: '', // Equivalent to `/` and alias for default view
            redirectTo: PaperEnrolmentRoutes.DEMOGRAPHIC,
            pathMatch: 'full'
          }
        ]
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PaperEnrolmentRoutingModule { }
