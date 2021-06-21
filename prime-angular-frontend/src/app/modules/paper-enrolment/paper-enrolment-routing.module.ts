import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { PaperEnrolmentRoutes } from '@paper-enrolment/paper-enrolment.routes';
import { DemographicComponent } from '@paper-enrolment/pages/demographic/demographic.component';
import { CanDeactivateFormGuard } from '@core/guards/can-deactivate-form.guard';
import { PaperEnrolmentDashboardComponent } from '@paper-enrolment/shared/components/paper-enrolment-dashboard/paper-enrolment-dashboard.component';
import { PaperEnrolmentGuard } from '@paper-enrolment/shared/guards/paper-enrolment.guard';
import { CareSettingComponent } from '@paper-enrolment/pages/care-setting/care-setting.component';

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
            data: { title: 'PRIME Profile' }
          },
          {
            path: PaperEnrolmentRoutes.CARE_SETTING,
            component: CareSettingComponent,
            canDeactivate: [CanDeactivateFormGuard],
            data: { title: 'PRIME Profile' }
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
