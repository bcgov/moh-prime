import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { CanDeactivateFormGuard } from '@core/guards/can-deactivate-form.guard';

import { PhsaLabtechDashboardComponent } from './shared/components/phsa-labtech-dashboard/phsa-labtech-dashboard.component';
import { PhsaLabtechGuard } from './shared/guards/phsa-labtech.guard';
import { AccessCodeComponent } from './pages/access-code/access-code.component';
import { BcscDemographicComponent } from './pages/bcsc-demographic/bcsc-demographic.component';
import { AuthenticationGuard } from '@auth/shared/guards/authentication.guard';
import { AvailableAccessComponent } from './pages/available-access/available-access.component';
import { SubmissionConfirmationComponent } from './pages/submission-confirmation/submission-confirmation.component';
import { PhsaLabtechRoutes } from './phsa-labtech.routes';


const routes: Routes = [
  {
    path: PhsaLabtechRoutes.MODULE_PATH,
    component: PhsaLabtechDashboardComponent,
    canActivate: [
      AuthenticationGuard
    ],
    canActivateChild: [
      PhsaLabtechGuard
    ],
    children: [
      {
        path: PhsaLabtechRoutes.ACCESS_CODE,
        component: AccessCodeComponent,
        canDeactivate: [CanDeactivateFormGuard],
        data: { title: 'Access Code' }
      },
      {
        path: PhsaLabtechRoutes.DEMOGRAPHIC,
        component: BcscDemographicComponent,
        data: { title: 'Enrolment' }
      },
      {
        path: PhsaLabtechRoutes.AVAILABLE_ACCESS,
        component: AvailableAccessComponent,
        data: { title: 'Available Access' }
      },
      {
        path: PhsaLabtechRoutes.SUBMISSION_CONFIRMATION,
        component: SubmissionConfirmationComponent,
        data: { title: 'Submission Confirmation' }
      },
      {
        path: '', // Equivalent to `/` and alias for the default route
        redirectTo: PhsaLabtechRoutes.DEMOGRAPHIC,
        pathMatch: 'full'
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PhsaLabtechRoutingModule { }
