import { NgModule } from '@angular/core';
import { ClipboardModule } from '@angular/cdk/clipboard';

import { NgxProgressModule } from '@lib/modules/ngx-progress/ngx-progress.module';
import { SharedModule } from '@shared/shared.module';

import { EnrolmentRoutingModule } from './enrolment-routing.module';

import { CollectionNoticeComponent } from './pages/collection-notice/collection-notice.component';
import { AccessCodeComponent } from './pages/access-code/access-code.component';
import { IdSubmissionComponent } from './pages/id-submission/id-submission.component';
import { BceidDemographicComponent } from './pages/bceid-demographic/bceid-demographic.component';
import { BcscDemographicComponent } from './pages/bcsc-demographic/bcsc-demographic.component';
import { RegulatoryComponent } from './pages/regulatory/regulatory.component';
import { DeviceProviderComponent } from './pages/device-provider/device-provider.component';
import { OboSitesPageComponent } from './pages/obo-sites-page/obo-sites-page.component';
import { RemoteAccessComponent } from './pages/remote-access/remote-access.component';
import { RemoteAccessAddressesComponent } from './pages/remote-access-addresses/remote-access-addresses.component';
import { SelfDeclarationComponent } from './pages/self-declaration/self-declaration.component';
import { CareSettingComponent } from './pages/care-setting/care-setting.component';
import { OverviewComponent } from './pages/overview/overview.component';
import { MinorUpdateConfirmationComponent } from './pages/minor-update-confirmation/minor-update-confirmation.component';
import { SubmissionConfirmationComponent } from './pages/submission-confirmation/submission-confirmation.component';
import { AccessAgreementComponent } from './pages/access-agreement/access-agreement.component';
import { PharmanetEnrolmentSummaryComponent } from './pages/pharmanet-enrolment-summary/pharmanet-enrolment-summary.component';
import { AccessLockedComponent } from './pages/access-locked/access-locked.component';
import { AccessDeclinedComponent } from './pages/access-declined/access-declined.component';
import { AccessAgreementHistoryComponent } from './pages/access-agreement-history/access-agreement-history.component';
import { AccessTermsComponent } from './pages/access-terms/access-terms.component';
import { AccessAgreementCurrentComponent } from './pages/access-agreement-current/access-agreement-current.component';
import {
  AccessAgreementHistoryEnrolmentComponent
} from './pages/access-agreement-history-enrolment/access-agreement-history-enrolment.component';
import { EnrolleePageComponent } from './shared/components/enrollee-page/enrollee-page.component';
import { EnrolmentCollectionNoticeComponent } from './shared/components/enrolment-collection-notice/enrolment-collection-notice.component';
import { DashboardV1Component } from './shared/components/dashboard/dashboardv1.component';
import { HeaderComponent } from './shared/components/header/header.component';
import { EnrolmentProgressIndicatorComponent } from './shared/components/enrolment-progress-indicator/enrolment-progress-indicator.component';
import { PaperEnrolleeReturneesPageComponent } from './pages/paper-enrollee-returnees-page/paper-enrollee-returnees-page.component';
import { PaperEnrolleeReturneeFormComponent } from './pages/paper-enrollee-returnees-page/paper-enrollee-returnee-form.component';

@NgModule({
  declarations: [
    DashboardV1Component,
    HeaderComponent,
    CollectionNoticeComponent,
    AccessCodeComponent,
    IdSubmissionComponent,
    BceidDemographicComponent,
    BcscDemographicComponent,
    RegulatoryComponent,
    DeviceProviderComponent,
    OboSitesPageComponent,
    RemoteAccessComponent,
    SelfDeclarationComponent,
    CareSettingComponent,
    OverviewComponent,
    MinorUpdateConfirmationComponent,
    SubmissionConfirmationComponent,
    AccessAgreementComponent,
    AccessDeclinedComponent,
    AccessLockedComponent,
    AccessAgreementHistoryComponent,
    PharmanetEnrolmentSummaryComponent,
    AccessTermsComponent,
    AccessAgreementCurrentComponent,
    AccessAgreementHistoryEnrolmentComponent,
    EnrolleePageComponent,
    EnrolmentCollectionNoticeComponent,
    EnrolmentProgressIndicatorComponent,
    RemoteAccessAddressesComponent,
    PaperEnrolleeReturneesPageComponent,
    PaperEnrolleeReturneeFormComponent
  ],
  imports: [
    SharedModule,
    EnrolmentRoutingModule,
    NgxProgressModule,
    ClipboardModule
  ]
})
export class EnrolmentModule { }
