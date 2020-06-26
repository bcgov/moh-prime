import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

import { NgxMaskModule } from 'ngx-mask';

import { ConfigModule } from '@config/config.module';
import { NgxBusyModule } from '@shared/modules/ngx-busy/ngx-busy.module';
import { NgxProgressModule } from '@shared/modules/ngx-progress/ngx-progress.module';
import { NgxMaterialModule } from '@shared/modules/ngx-material/ngx-material.module';
import { NgxContextualHelpModule } from '@shared/modules/ngx-contextual-help/ngx-contextual-help.module';

import { CapitalizePipe } from '@shared/pipes/capitalize.pipe';
import { EnrolleePipe } from '@shared/pipes/enrollee.pipe';
import { EnrolmentPipe } from '@shared/pipes/enrolment.pipe';
import { FirstKeyPipe } from '@shared/pipes/first-key.pipe';
import { FormatDatePipe } from '@shared/pipes/format-date.pipe';
import { PhonePipe } from '@shared/pipes/phone.pipe';
import { PostalPipe } from '@shared/pipes/postal.pipe';
import { ReplacePipe } from '@shared/pipes/replace.pipe';
import { CasePipe } from '@shared/pipes/case.pipe';
import { CertificatePipe } from '@shared/pipes/certificate.pipe';
import { DefaultPipe } from '@shared/pipes/default.pipe';
import { YesNoPipe } from '@shared/pipes/yes-no.pipe';
import { SafeHtmlPipe } from '@shared/pipes/safe-html.pipe';
import { WeekdayPipe } from '@shared/pipes/weekday.pipe';
import { FullnamePipe } from '@shared/pipes/fullname.pipe';
import { VendorPipe } from '@shared/pipes/vendor.pipe';

import { AddressComponent } from '@shared/components/forms/address/address.component';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import {
  EnrolmentStatusReasonsComponent
} from '@shared/components/dialogs/content/enrolment-status-reasons/enrolment-status-reasons.component';
import { DashboardComponent } from '@shared/components/dashboard/dashboard.component';
import { HeaderComponent } from '@shared/components/header/header.component';
import { PageComponent } from '@shared/components/page/page.component';
import { PageHeaderComponent } from '@shared/components/page-header/page-header.component';
import { ProgressIndicator2Component } from './components/progress-indicator2/progress-indicator2.component';
import { PageSubheaderComponent } from './components/page-subheader/page-subheader.component';
import { PageSubheaderTitleDirective } from '@shared/components/page-subheader/page-subheader-title.directive';
import { PageSubheaderSummaryDirective } from '@shared/components/page-subheader/page-subheader-summary.directive';
import { PageSubheader2Component } from './components/page-subheader2/page-subheader2.component';
import { PageSubheader2TitleDirective } from './components/page-subheader2/page-subheader2-title.directive';
import { PageSubheader2MoreInfoDirective } from './components/page-subheader2/page-subheader2-more-info.directive';
import { PageSubheader2SummaryDirective } from './components/page-subheader2/page-subheader2-summary.directive';
import { PageFooterComponent } from './components/page-footer/page-footer.component';
import { DialogContentDirective } from '@shared/components/dialogs/dialog-content.directive';
import { FormIconGroupComponent } from '@shared/components/form-icon-group/form-icon-group.component';
import { AlertComponent } from '@shared/components/alert/alert.component';
import { EnrolleePropertyComponent } from '@shared/components/enrollee/enrollee-property/enrollee-property.component';
import { EnrolleePropertyErrorComponent } from '@shared/components/enrollee/enrollee-property-error/enrollee-property-error.component';
import { EnrolleeReviewComponent } from '@shared/components/enrollee/enrollee-review/enrollee-review.component';
import { EnrolleeProfileComponent } from '@shared/components/enrollee/enrollee-profile/enrollee-profile.component';
import { PrimeEmailComponent } from '@shared/components/prime-email/prime-email.component';
import { PrimePhoneComponent } from '@shared/components/prime-phone/prime-phone.component';
import { PrimeLogoComponent } from '@shared/components/prime-logo/prime-logo.component';
import { ApproveEnrolmentComponent } from '@shared/components/dialogs/content/approve-enrolment/approve-enrolment.component';
import { PrimeSupportEmailComponent } from '@shared/components/prime-support-email/prime-support-email.component';
import { AccessTermsTableComponent } from '@shared/components/access-terms-table/access-terms-table.component';
import { AccessTermComponent } from '@shared/components/access-term/access-term.component';
import { NoteComponent } from '@shared/components/dialogs/content/note/note.component';
import { ClaimEnrolleeComponent } from '@shared/components/dialogs/content/claim-enrollee/claim-enrollee.component';
import { ManualFlagNoteComponent } from '@shared/components/dialogs/content/manual-flag-note/manual-flag-note.component';
import { FeedbackComponent } from '@shared/components/dialogs/content/feedback/feedback.component';
import { CollectionNoticeContainerComponent } from '@shared/components/collection-notice-container/collection-notice-container.component';
import { FormErrorsComponent } from '@shared/components/form-errors/form-errors.component';
import { SiteReviewComponent } from '@shared/components/site/site-review/site-review.component';
import { PartyReviewComponent } from '@shared/components/site/party-review/party-review.component';

@NgModule({
  declarations: [
    CapitalizePipe,
    CertificatePipe,
    DefaultPipe,
    EnrolleePipe,
    EnrolmentPipe,
    FirstKeyPipe,
    FormatDatePipe,
    PhonePipe,
    PostalPipe,
    ReplacePipe,
    YesNoPipe,
    WeekdayPipe,
    FullnamePipe,
    VendorPipe,
    CasePipe,
    SafeHtmlPipe,
    AddressComponent,
    DashboardComponent,
    ConfirmDialogComponent,
    EnrolmentStatusReasonsComponent,
    HeaderComponent,
    PageComponent,
    PageHeaderComponent,
    PageSubheaderComponent,
    PageSubheaderTitleDirective,
    PageSubheaderSummaryDirective,
    PageFooterComponent,
    DialogContentDirective,
    FormIconGroupComponent,
    AlertComponent,
    EnrolleeReviewComponent,
    EnrolleeProfileComponent,
    EnrolleePropertyComponent,
    EnrolleePropertyErrorComponent,
    PrimeEmailComponent,
    PrimePhoneComponent,
    PrimeLogoComponent,
    ApproveEnrolmentComponent,
    PrimeSupportEmailComponent,
    AccessTermsTableComponent,
    AccessTermComponent,
    NoteComponent,
    ClaimEnrolleeComponent,
    ManualFlagNoteComponent,
    FeedbackComponent,
    CollectionNoticeContainerComponent,
    FormErrorsComponent,
    SiteReviewComponent,
    PartyReviewComponent,
    ProgressIndicator2Component,
    PageSubheader2Component,
    PageSubheader2TitleDirective,
    PageSubheader2SummaryDirective,
    PageSubheader2MoreInfoDirective
  ],
  imports: [
    CommonModule,
    RouterModule,
    ConfigModule,
    NgxBusyModule,
    NgxContextualHelpModule,
    NgxMaskModule.forRoot(),
    NgxMaterialModule,
    NgxProgressModule,
    ReactiveFormsModule
  ],
  exports: [
    CommonModule,
    ConfigModule,
    NgxBusyModule,
    NgxContextualHelpModule,
    NgxMaterialModule,
    NgxMaskModule,
    NgxProgressModule,
    ReactiveFormsModule,
    CapitalizePipe,
    CasePipe,
    CertificatePipe,
    DefaultPipe,
    EnrolleePipe,
    EnrolmentPipe,
    FirstKeyPipe,
    FormatDatePipe,
    PhonePipe,
    PostalPipe,
    ReplacePipe,
    YesNoPipe,
    WeekdayPipe,
    FullnamePipe,
    SafeHtmlPipe,
    VendorPipe,
    AddressComponent,
    DashboardComponent,
    EnrolmentStatusReasonsComponent,
    HeaderComponent,
    PageComponent,
    PageHeaderComponent,
    ProgressIndicator2Component,
    PageSubheaderComponent,
    PageSubheaderTitleDirective,
    PageSubheaderSummaryDirective,
    PageSubheader2Component,
    PageSubheader2TitleDirective,
    PageSubheader2SummaryDirective,
    PageSubheader2MoreInfoDirective,
    PageFooterComponent,
    DialogContentDirective,
    FormIconGroupComponent,
    AlertComponent,
    EnrolleeReviewComponent,
    EnrolleeProfileComponent,
    EnrolleePropertyComponent,
    PrimeEmailComponent,
    PrimePhoneComponent,
    PrimeLogoComponent,
    PrimeSupportEmailComponent,
    AccessTermsTableComponent,
    AccessTermComponent,
    CollectionNoticeContainerComponent,
    FormErrorsComponent,
    SiteReviewComponent,
    PartyReviewComponent
  ]
})
export class SharedModule { }
