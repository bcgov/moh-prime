import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

// TODO split out all related filepond files into a /lib module ie. config and components
import { FilePondModule, registerPlugin } from 'ngx-filepond';
import FilePondPluginFileValidateType from 'filepond-plugin-file-validate-type';
import FilePondPluginFileValidateSize from 'filepond-plugin-file-validate-size';
registerPlugin(FilePondPluginFileValidateType, FilePondPluginFileValidateSize);
import { NgxMaskModule } from 'ngx-mask';

import { ConfigModule } from '@config/config.module';
import { NgxBusyModule } from '@lib/modules/ngx-busy/ngx-busy.module';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { NgxContextualHelpModule } from '@lib/modules/ngx-contextual-help/ngx-contextual-help.module';

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
import { WeekdayPipe } from '@shared/pipes/weekday.pipe';
import { FullnamePipe } from '@shared/pipes/fullname.pipe';
import { SafePipe } from '@shared/pipes/safe.pipe';
import { AddressPipe } from '@shared/pipes/address.pipe';
import { CareSettingPipe } from '@shared/pipes/care-setting.pipe';
import { JoinPipe } from '@shared/pipes/join.pipe';
import { PermissionPipe } from '@shared/pipes/permission-pipe';
import { AddressComponent } from '@shared/components/forms/address/address.component';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { PageComponent } from '@shared/components/page/page.component';
import { PageHeaderComponent } from '@shared/components/page-header/page-header.component';
import { ProgressIndicatorComponent } from '@shared/components/progress-indicator/progress-indicator.component';
import { PageSubheaderComponent } from '@shared/components/page-subheader/page-subheader.component';
import { PageSubheaderTitleDirective } from '@shared/components/page-subheader/page-subheader-title.directive';
import { PageSubheaderSummaryDirective } from '@shared/components/page-subheader/page-subheader-summary.directive';
import { PageSubheader2Component } from '@shared/components/page-subheader2/page-subheader2.component';
import { PageSubheader2TitleDirective } from '@shared/components/page-subheader2/page-subheader2-title.directive';
import { PageSubheader2MoreInfoDirective } from '@shared/components/page-subheader2/page-subheader2-more-info.directive';
import { PageSubheader2SummaryDirective } from '@shared/components/page-subheader2/page-subheader2-summary.directive';
import { NotificationInfoSummaryDirective } from '@shared/components/forms/contact-information-form/notification-info-summary.directive';
import { PageFooterComponent } from '@shared/components/page-footer/page-footer.component';
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
import { ManualFlagNoteComponent } from '@shared/components/dialogs/content/manual-flag-note/manual-flag-note.component';
import { NoteComponent } from '@shared/components/dialogs/content/note/note.component';
import { CollectionNoticeContainerComponent } from '@shared/components/collection-notice-container/collection-notice-container.component';
import { FormErrorsComponent } from '@shared/components/form-errors/form-errors.component';
import { PartyReviewComponent } from '@shared/components/site/party-review/party-review.component';
import { DocumentUploadComponent } from '@shared/components/document-upload/document-upload/document-upload.component';
import { ImageComponent } from '@shared/components/dialogs/content/image/image.component';
import { OverviewSectionComponent } from '@shared/components/overview-section/overview-section.component';
import { OverviewContainerComponent } from '@shared/components/site/overview-container/overview-container.component';
import { CollegeCertificationFormComponent } from '@shared/components/forms/college-certification-form/college-certification-form.component';
import { AddressAutocompleteComponent } from '@shared/components/address-autocomplete/address-autocomplete.component';
import { RemoteUserReviewComponent } from '@shared/components/site/remote-user-review/remote-user-review.component';
import { AccessCodeFormComponent } from '@shared/components/forms/access-code-form/access-code-form.component';
import { ContactInformationFormComponent } from '@shared/components/forms/contact-information-form/contact-information-form.component';
import { EnrolleeSelfDeclarationsComponent } from '@shared/components/enrollee/enrollee-self-declarations/enrollee-self-declarations.component';
import { TriageComponent } from './components/dialogs/content/triage/triage.component';
import { SendEmailComponent } from '@shared/components/dialogs/content/send-email/send-email.component';
import { EscalationNoteComponent } from './components/dialogs/content/escalation-note/escalation-note.component';
import { ClaimNoteComponent } from './components/dialogs/content/claim-note/claim-note.component';

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
    SafePipe,
    CasePipe,
    AddressPipe,
    PermissionPipe,
    AddressComponent,
    ConfirmDialogComponent,
    PageComponent,
    ProgressIndicatorComponent,
    PageHeaderComponent,
    PageSubheaderComponent,
    PageSubheaderTitleDirective,
    PageSubheaderSummaryDirective,
    PageSubheader2Component,
    PageSubheader2TitleDirective,
    PageSubheader2SummaryDirective,
    PageSubheader2MoreInfoDirective,
    NotificationInfoSummaryDirective,
    PageFooterComponent,
    DialogContentDirective,
    FormIconGroupComponent,
    AlertComponent,
    EnrolleeReviewComponent,
    EnrolleeSelfDeclarationsComponent,
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
    ManualFlagNoteComponent,
    ImageComponent,
    CollectionNoticeContainerComponent,
    FormErrorsComponent,
    PartyReviewComponent,
    DocumentUploadComponent,
    ImageComponent,
    OverviewSectionComponent,
    OverviewContainerComponent,
    CollegeCertificationFormComponent,
    AddressAutocompleteComponent,
    RemoteUserReviewComponent,
    AccessCodeFormComponent,
    ContactInformationFormComponent,
    SendEmailComponent,
    CareSettingPipe,
    JoinPipe,
    TriageComponent,
    EscalationNoteComponent,
    ClaimNoteComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    ConfigModule,
    NgxBusyModule,
    NgxContextualHelpModule,
    NgxMaskModule.forRoot(),
    NgxMaterialModule,
    FilePondModule,
    ReactiveFormsModule
  ],
  exports: [
    CommonModule,
    ConfigModule,
    NgxBusyModule,
    NgxContextualHelpModule,
    NgxMaterialModule,
    NgxMaskModule,
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
    SafePipe,
    AddressPipe,
    PermissionPipe,
    AddressComponent,
    PageComponent,
    PageHeaderComponent,
    ProgressIndicatorComponent,
    PageSubheaderComponent,
    PageSubheaderTitleDirective,
    PageSubheaderSummaryDirective,
    PageSubheader2Component,
    PageSubheader2TitleDirective,
    PageSubheader2SummaryDirective,
    PageSubheader2MoreInfoDirective,
    NotificationInfoSummaryDirective,
    PageFooterComponent,
    DialogContentDirective,
    FormIconGroupComponent,
    AlertComponent,
    EnrolleeReviewComponent,
    EnrolleeSelfDeclarationsComponent,
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
    PartyReviewComponent,
    DocumentUploadComponent,
    OverviewSectionComponent,
    OverviewContainerComponent,
    CollegeCertificationFormComponent,
    RemoteUserReviewComponent,
    AccessCodeFormComponent,
    ContactInformationFormComponent,
    CareSettingPipe,
    JoinPipe,
    TriageComponent,
    EscalationNoteComponent,
    ClaimNoteComponent
  ],
  providers: [
    FullnamePipe,
    AddressPipe,
    CasePipe,
    CapitalizePipe
  ]
})
export class SharedModule { }
