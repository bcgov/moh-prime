import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AuthenticationGuard } from '@auth/shared/guards/authentication.guard';
import { CanDeactivateFormGuard } from '@core/guards/can-deactivate-form.guard';

import { SatEformsRoutes } from './sat-eforms.routes';
import { SatEformsDashboardComponent } from './shared/components/sat-eforms-dashboard/sat-eforms-dashboard.component';

import { CollectionNoticePageComponent } from '@sat/pages/collection-notice-page/collection-notice-page.component';
import { DemographicPageComponent } from '@sat/pages/demographic-page/demographic-page.component';
import { SubmissionConfirmationPageComponent } from '@sat/pages/submission-confirmation-page/submission-confirmation-page.component';
import { SatEformsGuard } from '@sat/shared/guards/sat-eforms.guard';

const routes: Routes = [
  {
    path: '',
    component: SatEformsDashboardComponent,
    canActivate: [AuthenticationGuard],
    canActivateChild: [AuthenticationGuard],
    children: [
      {
        path: SatEformsRoutes.COLLECTION_NOTICE,
        component: CollectionNoticePageComponent,
        data: { title: 'Collection Notice' }
      },
      {
        path: `${SatEformsRoutes.ENROLMENTS}/:eid`,
        canActivateChild: [SatEformsGuard],
        children: [
          {
            path: SatEformsRoutes.DEMOGRAPHIC,
            component: DemographicPageComponent,
            canDeactivate: [CanDeactivateFormGuard],
            data: { title: 'Special Authority E-Forms' }
          },
          {
            path: SatEformsRoutes.SUBMISSION_CONFIRMATION,
            component: SubmissionConfirmationPageComponent,
            data: { title: 'Special Authority E-Forms' }
          }
        ]
      },
      {
        path: '', // Equivalent to `/` and alias for `collection-notice`
        redirectTo: SatEformsRoutes.COLLECTION_NOTICE,
        pathMatch: 'full'
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SatEformsRoutingModule { }
