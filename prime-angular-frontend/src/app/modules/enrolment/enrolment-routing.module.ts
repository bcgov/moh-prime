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
import { ConfirmationComponent } from './pages/confirmation/confirmation.component';
import { AccessAgreementComponent } from './pages/access-agreement/access-agreement.component';
import { CollectionNoticeComponent } from './pages/collection-notice/collection-notice.component';
import { DeclinedComponent } from './pages/declined/declined.component';
import { DeclinedAccessAgreementComponent } from './pages/declined-access-agreement/declined-access-agreement.component';
import { AccessAgreementHistoryComponent } from './pages/access-agreement-history/access-agreement-history.component';
import { PharmanetEnrolmentCertificateComponent } from './pages/pharmanet-enrolment-certificate/pharmanet-enrolment-certificate.component';
import { PharmanetTransactionsComponent } from './pages/pharmanet-transactions/pharmanet-transactions.component';
import { EnrolmentLogHistoryComponent } from './pages/enrolment-log-history/enrolment-log-history.component';

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
      //
      // Enrollee overview:
      //
      {
        // Overview of the enrollee profile for viewing, and
        // reviewing prior to submission
        path: EnrolmentRoutes.OVERVIEW,
        component: OverviewComponent,
        data: { title: 'PharmaNet Enrolment' }
      },
      //
      // Enrollee profile:
      //
      // TODO refactor routes to have deeper child routing, which
      // will provide an easier way to lock down routing, and provide
      // feedback for marking active routes in the dashboard
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
      // TODO Temporary removal of device provider for ComPAP
      {
        path: EnrolmentRoutes.DEVICE_PROVIDER,
        component: DeviceProviderComponent,
        canDeactivate: [CanDeactivateFormGuard],
        data: { title: 'PharmaNet Enrolment' }
      },
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
      //
      // Enrolment submission:
      //
      {
        path: EnrolmentRoutes.SUBMISSION_CONFIRMATION,
        component: ConfirmationComponent,
        data: { title: 'Enrolment Confirmation' }
      },
      {
        path: EnrolmentRoutes.DECLINED,
        component: DeclinedComponent,
        data: { title: 'Enrolment Summary' }
      },
      {
        path: EnrolmentRoutes.ACCESS_AGREEMENT,
        component: AccessAgreementComponent,
        data: { title: 'Enrolment Access Agreement' }
      },
      {
        path: EnrolmentRoutes.DECLINED_ACCESS_AGREEMENT,
        component: DeclinedAccessAgreementComponent,
        data: { title: 'Enrolment Summary' }
      },
      //
      // Enrollee history and PharmaNet:
      //
      {
        path: EnrolmentRoutes.ACCESS_AGREEMENT_HISTORY,
        component: AccessAgreementHistoryComponent,
        data: { title: 'Access Agreement History' }
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
        path: EnrolmentRoutes.ENROLMENT_LOG_HISTORY,
        component: EnrolmentLogHistoryComponent,
        data: { title: 'Enrolment Log History' }
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
