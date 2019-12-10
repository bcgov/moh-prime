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
import { DeclinedComponent } from './pages/declined/declined.component';
import { DeclinedAccessAgreementComponent } from './pages/declined-access-agreement/declined-access-agreement.component';

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
        data: { title: 'Collection Notice' }
      },
      {
        path: EnrolmentRoutes.PROFILE,
        component: ProfileComponent,
        canDeactivate: [CanDeactivateFormGuard],
        data: { title: 'PharmaNet Enrolment' }
      },
      {
        path: EnrolmentRoutes.REGULATORY,
        component: RegulatoryComponent,
        canDeactivate: [CanDeactivateFormGuard],
        data: { title: 'PharmaNet Enrolment' }
      },
      // TODO Temporary removal of Device Provider for ComPAP
      // {
      //   path: EnrolmentRoutes.DEVICE_PROVIDER,
      //   component: DeviceProviderComponent,
      //   canDeactivate: [CanDeactivateFormGuard],
      //   data: { title: 'PharmaNet Enrolment' }
      // },
      {
        path: EnrolmentRoutes.JOB,
        component: JobComponent,
        canDeactivate: [CanDeactivateFormGuard],
        data: { title: 'PharmaNet Enrolment' }
      },
      {
        path: EnrolmentRoutes.SELF_DECLARATION,
        component: SelfDeclarationComponent,
        canDeactivate: [CanDeactivateFormGuard],
        data: { title: 'PharmaNet Enrolment' }
      },
      {
        path: EnrolmentRoutes.ORGANIZATION,
        component: OrganizationComponent,
        canDeactivate: [CanDeactivateFormGuard],
        data: { title: 'PharmaNet Enrolment' }
      },
      {
        path: EnrolmentRoutes.REVIEW,
        component: ReviewComponent,
        data: { title: 'PharmaNet Enrolment' }
      },
      {
        path: EnrolmentRoutes.CONFIRMATION,
        component: ConfirmationComponent,
        data: { title: 'Enrolment Confirmation' }
      },
      {
        path: EnrolmentRoutes.ACCESS_AGREEMENT,
        component: AccessAgreementComponent,
        data: { title: 'Enrolment Access Agreement' }
      },
      {
        path: EnrolmentRoutes.SUMMARY,
        component: SummaryComponent,
        data: { title: 'Enrolment Summary' }
      },
      {
        path: EnrolmentRoutes.DECLINED,
        component: DeclinedComponent,
        data: { title: 'Enrolment Summary' }
      },
      {
        path: EnrolmentRoutes.DECLINED_ACCESS_AGREEMENT,
        component: DeclinedAccessAgreementComponent,
        data: { title: 'Enrolment Summary' }
      },
      {
        path: '', // Equivalent to `/` and alias for `review`
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
