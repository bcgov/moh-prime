import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ConfigResolver } from '@config/config-resolver';
import { CanDeactivateFormGuard } from '@core/guards/can-deactivate-form.guard';
import { DashboardComponent } from '@shared/components/dashboard/dashboard.component';
import { AuthenticationGuard } from '@auth/shared/guards/authentication.guard';

import { EnrolmentRoutes } from './enrolment.routes';
import { EnrolleeGuard } from './shared/guards/enrollee.guard';
import { EnrolmentGuard } from './shared/guards/enrolment.guard';

import { ProfileComponent } from './pages/profile/profile.component';
import { RegulatoryComponent } from './pages/regulatory/regulatory.component';
import { DeviceProviderComponent } from './pages/device-provider/device-provider.component';
import { JobComponent } from './pages/job/job.component';
import { SelfDeclarationComponent } from './pages/self-declaration/self-declaration.component';
import { OrganizationComponent } from './pages/organization/organization.component';
import { ReviewComponent } from './pages/review/review.component';
import { ConfirmationComponent } from './pages/confirmation/confirmation.component';
import { AccessAgreementComponent } from './pages/access-agreement/access-agreement.component';
import { SummaryComponent } from './pages/summary/summary.component';
import { CollectionNoticeComponent } from './pages/collection-notice/collection-notice.component';

const routes: Routes = [
  {
    path: EnrolmentRoutes.MODULE_PATH,
    component: DashboardComponent,
    canActivateChild: [
      AuthenticationGuard,
      EnrolleeGuard,
      EnrolmentGuard
    ],
    resolve: [ConfigResolver],
    children: [
      {
        path: EnrolmentRoutes.COLLECTION_NOTICE,
        component: CollectionNoticeComponent,
      },
      {
        path: EnrolmentRoutes.PROFILE,
        component: ProfileComponent,
        canDeactivate: [CanDeactivateFormGuard]
      },
      {
        path: EnrolmentRoutes.REGULATORY,
        component: RegulatoryComponent,
        canDeactivate: [CanDeactivateFormGuard]
      },
      // TODO Temporary removal of Device Provider for ComPAP
      // {
      //   path: EnrolmentRoutes.DEVICE_PROVIDER,
      //   component: DeviceProviderComponent,
      //   canDeactivate: [CanDeactivateFormGuard]
      // },
      {
        path: EnrolmentRoutes.JOB,
        component: JobComponent,
        canDeactivate: [CanDeactivateFormGuard]
      },
      {
        path: EnrolmentRoutes.SELF_DECLARATION,
        component: SelfDeclarationComponent,
        canDeactivate: [CanDeactivateFormGuard]
      },
      {
        path: EnrolmentRoutes.ORGANIZATION,
        component: OrganizationComponent,
        canDeactivate: [CanDeactivateFormGuard]
      },
      {
        path: EnrolmentRoutes.REVIEW,
        component: ReviewComponent
      },
      {
        path: EnrolmentRoutes.CONFIRMATION,
        component: ConfirmationComponent
      },
      {
        path: EnrolmentRoutes.ACCESS_AGREEMENT,
        component: AccessAgreementComponent
      },
      {
        path: EnrolmentRoutes.SUMMARY,
        component: SummaryComponent
      },
      {
        path: '', // Equivalent to `/` and alias for `profile`
        redirectTo: EnrolmentRoutes.REVIEW,
        pathMatch: 'full'
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EnrolmentRoutingModule { }
