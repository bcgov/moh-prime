import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { CanDeactivateFormGuard } from '@core/guards/can-deactivate-form.guard';

import { AuthenticationGuard } from '@auth/shared/guards/authentication.guard';

import { PhsaEformsRoutes } from './phsa-eforms.routes';
import { PhsaEformsGuard } from './shared/guards/phsa-eforms.guard';
import { PhsaEformsDashboardComponent } from './shared/components/phsa-eforms-dashboard/phsa-eforms-dashboard.component';

import { AccessCodeComponent } from './pages/access-code/access-code.component';
import { BcscDemographicComponent } from './pages/bcsc-demographic/bcsc-demographic.component';
import { AvailableAccessComponent } from './pages/available-access/available-access.component';
import { SubmissionConfirmationComponent } from './pages/submission-confirmation/submission-confirmation.component';
import { CollectionNoticeComponent } from './pages/collection-notice/collection-notice.component';

const routes: Routes = [
  {
    path: '',
    component: PhsaEformsDashboardComponent,
    canActivate: [
      AuthenticationGuard
    ],
    canActivateChild: [
      AuthenticationGuard,
      PhsaEformsGuard
    ],
    children: [
      {
        path: PhsaEformsRoutes.COLLECTION_NOTICE,
        component: CollectionNoticeComponent,
        data: { title: 'Collection Notice' }
      },
      {
        path: PhsaEformsRoutes.ACCESS_CODE,
        component: AccessCodeComponent,
        canDeactivate: [CanDeactivateFormGuard],
        data: { title: 'Access Code' }
      },
      {
        path: PhsaEformsRoutes.DEMOGRAPHIC,
        component: BcscDemographicComponent,
        data: { title: 'Enrolment' }
      },
      {
        path: PhsaEformsRoutes.AVAILABLE_ACCESS,
        component: AvailableAccessComponent,
        data: { title: 'Available Access' }
      },
      {
        path: PhsaEformsRoutes.SUBMISSION_CONFIRMATION,
        component: SubmissionConfirmationComponent,
        data: { title: 'Submission Confirmation' }
      },
      {
        path: '', // Equivalent to `/` and alias for the default route
        redirectTo: PhsaEformsRoutes.DEMOGRAPHIC,
        pathMatch: 'full'
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PhsaEformsRoutingModule { }
