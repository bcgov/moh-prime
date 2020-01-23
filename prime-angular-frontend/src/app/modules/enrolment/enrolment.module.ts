import { NgModule } from '@angular/core';

import { SharedModule } from '@shared/shared.module';
import { EnrolmentRoutingModule } from './enrolment-routing.module';

import { CollectionNoticeComponent } from './pages/collection-notice/collection-notice.component';
import { DemographicComponent } from './pages/demographic/demographic.component';
import { RegulatoryComponent } from './pages/regulatory/regulatory.component';
import { DeviceProviderComponent } from './pages/device-provider/device-provider.component';
import { JobComponent } from './pages/job/job.component';
import { SelfDeclarationComponent } from './pages/self-declaration/self-declaration.component';
import { OrganizationComponent } from './pages/organization/organization.component';
import { OverviewComponent } from './pages/overview/overview.component';
import { SubmissionConfirmationComponent } from './pages/submission-confirmation/submission-confirmation.component';
import { AccessAgreementComponent } from './pages/access-agreement/access-agreement.component';
import { PageRefDirective } from './pages/access-agreement/page-ref.directive';
import { AccessTermsPagerComponent } from './pages/access-agreement/components/access-terms-pager/access-terms-pager.component';
import { GlobalClauseComponent } from './pages/access-agreement/components/global-clause/global-clause.component';
import { LicenceClassClauseComponent } from './pages/access-agreement/components/licence-class-clause/licence-class-clause.component';
import {
  LimitsAndConditionsClauseComponent
} from './pages/access-agreement/components/limits-and-conditions-clause/limits-and-conditions-clause.component';
import { UserClauseComponent } from './pages/access-agreement/components/user-clause/user-clause.component';
import { PharmanetEnrolmentCertificateComponent } from './pages/pharmanet-enrolment-certificate/pharmanet-enrolment-certificate.component';
import { DeclinedComponent } from './pages/declined/declined.component';
import { DeclinedAccessAgreementComponent } from './pages/declined-access-agreement/declined-access-agreement.component';
import { AccessAgreementHistoryComponent } from './pages/access-agreement-history/access-agreement-history.component';
import { PharmanetTransactionsComponent } from './pages/pharmanet-transactions/pharmanet-transactions.component';
import { EnrolmentLogHistoryComponent } from './pages/enrolment-log-history/enrolment-log-history.component';

import { PageFooterComponent } from './shared/components/page-footer/page-footer.component';
import { CollegeCertificationFormComponent } from './shared/components/college-certification-form/college-certification-form.component';
import { JobFormComponent } from './shared/components/job-form/job-form.component';
import { CollectionNoticeAlertComponent } from './shared/components/collection-notice-alert/collection-notice-alert.component';
import { RuAccessTermsComponent } from './pages/access-agreement/components/ru-access-terms/ru-access-terms.component';
import { OboAccessTermsComponent } from './pages/access-agreement/components/obo-access-terms/obo-access-terms.component';

@NgModule({
  declarations: [
    CollectionNoticeComponent,
    DemographicComponent,
    RegulatoryComponent,
    DeviceProviderComponent,
    JobComponent,
    SelfDeclarationComponent,
    OrganizationComponent,
    OverviewComponent,
    SubmissionConfirmationComponent,
    AccessAgreementComponent,
    PageRefDirective,
    AccessTermsPagerComponent,
    GlobalClauseComponent,
    LicenceClassClauseComponent,
    LimitsAndConditionsClauseComponent,
    UserClauseComponent,
    DeclinedComponent,
    DeclinedAccessAgreementComponent,
    AccessAgreementHistoryComponent,
    PharmanetEnrolmentCertificateComponent,
    PharmanetTransactionsComponent,
    EnrolmentLogHistoryComponent,
    PageFooterComponent,
    CollegeCertificationFormComponent,
    JobFormComponent,
    CollectionNoticeAlertComponent,
    OboAccessTermsComponent,
    RuAccessTermsComponent
  ],
  imports: [
    SharedModule,
    EnrolmentRoutingModule
  ],
  exports: []
})
export class EnrolmentModule { }
