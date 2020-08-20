import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ConfigResolver } from '@config/config-resolver';
import { UnsupportedGuard } from '@core/guards/unsupported.guard';
import { CanDeactivateFormGuard } from '@core/guards/can-deactivate-form.guard';
import { AuthenticationGuard } from '@auth/shared/guards/authentication.guard';

import { EnrolmentRoutes } from './enrolment.routes';
import { EnrolleeGuard } from './shared/guards/enrollee.guard';
import { EnrolmentGuard } from './shared/guards/enrolment.guard';
import { DashboardV1Component } from './shared/components/dashboard/dashboardv1.component';

import { OverviewComponent } from './pages/overview/overview.component';
import { DemographicComponent } from './pages/demographic/demographic.component';
import { RegulatoryComponent } from './pages/regulatory/regulatory.component';
// TODO Temporary removal of device provider for Community Practice
// import { DeviceProviderComponent } from './pages/device-provider/device-provider.component';
import { JobComponent } from './pages/job/job.component';
import { SelfDeclarationComponent } from './pages/self-declaration/self-declaration.component';
import { CareSettingComponent } from './pages/care-setting/care-setting.component';
import { SubmissionConfirmationComponent } from './pages/submission-confirmation/submission-confirmation.component';
import { AccessAgreementComponent } from './pages/access-agreement/access-agreement.component';
import { CollectionNoticeComponent } from './pages/collection-notice/collection-notice.component';
import { AccessLockedComponent } from './pages/access-locked/access-locked.component';
import { AccessAgreementHistoryComponent } from './pages/access-agreement-history/access-agreement-history.component';
import { PharmanetEnrolmentSummaryComponent } from './pages/pharmanet-enrolment-summary/pharmanet-enrolment-summary.component';
import { AccessTermsComponent } from './pages/access-terms/access-terms.component';
import { AccessAgreementCurrentComponent } from './pages/access-agreement-current/access-agreement-current.component';
import {
  AccessAgreementHistoryEnrolmentComponent
} from './pages/access-agreement-history-enrolment/access-agreement-history-enrolment.component';
import { MinorUpdateConfirmationComponent } from './pages/minor-update-confirmation/minor-update-confirmation.component';
import { AccessDeclinedComponent } from './pages/access-declined/access-declined.component';
import { NotificationConfirmationComponent } from './pages/notification-confirmation/notification-confirmation.component';

const routes: Routes = [
  {
    path: EnrolmentRoutes.MODULE_PATH,
    component: DashboardV1Component,
    canActivate: [UnsupportedGuard],
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
        data: { title: 'PRIME Enrolment' }
      },
      //
      // Enrollee profile:
      //
      {
        path: EnrolmentRoutes.DEMOGRAPHIC,
        component: DemographicComponent,
        canDeactivate: [CanDeactivateFormGuard],
        data: { title: 'PRIME Enrolment' }
      },
      {
        path: EnrolmentRoutes.REGULATORY,
        component: RegulatoryComponent,
        canDeactivate: [CanDeactivateFormGuard],
        data: { title: 'PRIME Enrolment' }
      },
      // TODO Temporary removal of device provider for Community Practice
      // {
      //   path: EnrolmentRoutes.DEVICE_PROVIDER,
      //   component: DeviceProviderComponent,
      //   canDeactivate: [CanDeactivateFormGuard],
      //   data: { title: 'PRIME Enrolment' }
      // },
      {
        path: EnrolmentRoutes.JOB,
        component: JobComponent,
        canDeactivate: [CanDeactivateFormGuard],
        data: { title: 'PRIME Enrolment' }
      },
      {
        path: EnrolmentRoutes.CARE_SETTING,
        component: CareSettingComponent,
        canDeactivate: [CanDeactivateFormGuard],
        data: { title: 'PRIME Enrolment' }
      },
      {
        path: EnrolmentRoutes.SELF_DECLARATION,
        component: SelfDeclarationComponent,
        canDeactivate: [CanDeactivateFormGuard],
        data: { title: 'PRIME Enrolment' }
      },
      //
      // Enrolment submission:
      //
      {
        path: EnrolmentRoutes.CHANGES_SAVED,
        component: MinorUpdateConfirmationComponent,
        data: { title: 'PRIME Enrolment' }
      },
      {
        path: EnrolmentRoutes.SUBMISSION_CONFIRMATION,
        component: SubmissionConfirmationComponent,
        data: { title: 'PRIME Enrolment' }
      },
      {
        path: EnrolmentRoutes.ACCESS_LOCKED,
        component: AccessLockedComponent,
        data: { title: 'Enrolment Summary' }
      },
      {
        path: EnrolmentRoutes.ACCESS_DECLINED,
        component: AccessDeclinedComponent,
        data: { title: 'Enrolment Summary' }
      },
      {
        path: EnrolmentRoutes.PENDING_ACCESS_TERM,
        component: AccessAgreementComponent,
        data: { title: 'Terms of Access' }
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
        path: EnrolmentRoutes.PHARMANET_ENROLMENT_SUMMARY,
        component: PharmanetEnrolmentSummaryComponent,
        data: { title: 'Next Steps to get PharmaNet' }
      },
      {
        path: EnrolmentRoutes.NOTIFICATION_CONFIRMATION,
        component: NotificationConfirmationComponent,
        data: { title: 'Notification Confirmation' }
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
