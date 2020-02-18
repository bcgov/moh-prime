import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ConfigResolver } from '@config/config-resolver';
import { CanDeactivateFormGuard } from '@core/guards/can-deactivate-form.guard';
import { DashboardComponent } from '@shared/components/dashboard/dashboard.component';
import { AuthenticationGuard } from '@auth/shared/guards/authentication.guard';

import { EnrolmentRoutes } from './enrolment.routes';
import { EnrolleeGuard } from './shared/guards/enrollee.guard';
import { EnrolmentGuard } from './shared/guards/enrolment.guard';

import { OverviewComponent } from './pages/overview/overview.component';
import { DemographicComponent } from './pages/demographic/demographic.component';
import { RegulatoryComponent } from './pages/regulatory/regulatory.component';
import { DeviceProviderComponent } from './pages/device-provider/device-provider.component';
import { JobComponent } from './pages/job/job.component';
import { SelfDeclarationComponent } from './pages/self-declaration/self-declaration.component';
import { OrganizationComponent } from './pages/organization/organization.component';
import { SubmissionConfirmationComponent } from './pages/submission-confirmation/submission-confirmation.component';
import { AccessAgreementComponent } from './pages/access-agreement/access-agreement.component';
import { CollectionNoticeComponent } from './pages/collection-notice/collection-notice.component';
import { AccessLocked } from './pages/access-locked/access-locked.component';
import { AccessAgreementHistoryComponent } from './pages/access-agreement-history/access-agreement-history.component';
import { PharmanetEnrolmentCertificateComponent } from './pages/pharmanet-enrolment-certificate/pharmanet-enrolment-certificate.component';
import { PharmanetTransactionsComponent } from './pages/pharmanet-transactions/pharmanet-transactions.component';
import { AccessTermsComponent } from './pages/access-terms/access-terms.component';
import { AccessAgreementCurrentComponent } from './pages/access-agreement-current/access-agreement-current.component';
import { AccessAgreementHistoryEnrolmentComponent } from './pages/access-agreement-history-enrolment/access-agreement-history-enrolment.component';

const routes: Routes = [
  {
    path: EnrolmentRoutes.MODULE_PATH,
    component: DashboardComponent,
    canActivateChild: [
      AuthenticationGuard,
      EnrolleeGuard,
      EnrolmentGuard
    ],
    // Ensure that the configuration is loaded, otherwise
    // if it already exists NOOP
    resolve: [ConfigResolver],
    children: [
      {
        path: EnrolmentRoutes.COLLECTION_NOTICE,
        component: CollectionNoticeComponent,
        data: { title: 'Collection Notice' }
      },
      //
      // Enrollee overview:
      //
      {
        // Overview of the enrollee profile for viewing, and
        // reviewing prior to submission
        path: EnrolmentRoutes.OVERVIEW,
        component: OverviewComponent,
        canDeactivate: [CanDeactivateFormGuard],
        data: { title: 'PharmaNet Enrolment' }
      },
      //
      // Enrollee profile:
      //
      {
        path: EnrolmentRoutes.DEMOGRAPHIC,
        component: DemographicComponent,
        canDeactivate: [CanDeactivateFormGuard],
        data: { title: 'PharmaNet Enrolment' }
      },
      {
        path: EnrolmentRoutes.REGULATORY,
        component: RegulatoryComponent,
        canDeactivate: [CanDeactivateFormGuard],
        data: { title: 'PharmaNet Enrolment' }
      },
      // TODO Temporary removal of device provider for Community Practice
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
        path: EnrolmentRoutes.ORGANIZATION,
        component: OrganizationComponent,
        canDeactivate: [CanDeactivateFormGuard],
        data: { title: 'PharmaNet Enrolment' }
      },
      {
        path: EnrolmentRoutes.SELF_DECLARATION,
        component: SelfDeclarationComponent,
        canDeactivate: [CanDeactivateFormGuard],
        data: { title: 'PharmaNet Enrolment' }
      },
      //
      // Enrolment submission:
      //
      {
        path: EnrolmentRoutes.SUBMISSION_CONFIRMATION,
        component: SubmissionConfirmationComponent,
        data: { title: 'PharmaNet Enrolment' }
      },
      {
        path: EnrolmentRoutes.ACCESS_LOCKED,
        component: AccessLocked,
        data: { title: 'Enrolment Summary' }
      },
      {
        path: EnrolmentRoutes.PENDING_ACCESS_TERM,
        component: AccessAgreementComponent,
        data: { title: 'Enrolment Terms of Access' }
      },
      //
      // Enrollee history and PharmaNet:
      //
      {
        path: EnrolmentRoutes.CURRENT_ACCESS_TERM,
        component: AccessAgreementCurrentComponent,
        data: { title: 'Terms of Access' }
      },
      {
        path: EnrolmentRoutes.PHARMANET_ENROLMENT_CERTIFICATE,
        component: PharmanetEnrolmentCertificateComponent,
        data: { title: 'PharmaNet Enrolment Certificate' }
      },
      {
        path: EnrolmentRoutes.PHARMANET_TRANSACTIONS,
        component: PharmanetTransactionsComponent,
        data: { title: 'PharmaNet Transactions' }
      },
      {
        path: EnrolmentRoutes.ACCESS_TERMS,
        children: [
          {
            path: '',
            component: AccessTermsComponent,
            data: { title: 'PRIME History' }
          },
          {
            path: ':id',
            children: [
              {
                path: '',
                component: AccessAgreementHistoryComponent,
                data: { title: 'PRIME History' }
              },
              {
                path: EnrolmentRoutes.ENROLMENT,
                component: AccessAgreementHistoryEnrolmentComponent,
                data: { title: 'PRIME History' }
              },
            ]
          }
        ]
      },
      {
        path: '', // Equivalent to `/` and alias for `overview`
        redirectTo: EnrolmentRoutes.OVERVIEW,
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
