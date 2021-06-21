import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { PaperEnrolmentRoutes } from '@paper-enrolment/paper-enrolment.routes';
import { DemographicComponent } from '@paper-enrolment/pages/demographic/demographic.component';
import { CanDeactivateFormGuard } from '@core/guards/can-deactivate-form.guard';
import { PaperEnrolmentDashboardComponent } from './shared/components/paper-enrolment-dashboard/paper-enrolment-dashboard.component';

const routes: Routes = [
  {
    path: '',
    component: PaperEnrolmentDashboardComponent,
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
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PaperEnrolmentRoutingModule { }
