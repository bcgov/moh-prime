import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AuthenticationGuard } from '@auth/shared/guards/authentication.guard';


import { PaperEnrolmentRoutes } from '@paper-enrolment/paper-enrolment.routes';
import { DemographicComponent } from '@paper-enrolment/pages/demographic/demographic.component';
import { AdjudicationGuard } from '@adjudication/shared/guards/adjudication.guard';
import { CanDeactivateFormGuard } from '@core/guards/can-deactivate-form.guard';
import { PaperEnrolmentDashboardComponent } from './shared/components/paper-enrolment-dashboard/paper-enrolment-dashboard.component';

const routes: Routes = [
  {
    path: PaperEnrolmentRoutes.MODULE_PATH,
    component: PaperEnrolmentDashboardComponent,
    children: [
      {
        path: PaperEnrolmentRoutes.DEMOGRAPHIC,
        component: DemographicComponent,
        canDeactivate: [CanDeactivateFormGuard],
        data: { title: 'PRIME Enrolment' }
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PaperEnrolmentRoutingModule { }
