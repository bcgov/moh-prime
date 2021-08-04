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
import { FormatDatePipe } from '@shared/pipes/format-date.pipe';
import { PhonePipe } from '@shared/pipes/phone.pipe';
import { PostalPipe } from '@shared/pipes/postal.pipe';
import { ReplacePipe } from '@shared/pipes/replace.pipe';
import { CasePipe } from '@shared/pipes/case.pipe';
import { DefaultPipe } from '@shared/pipes/default.pipe';
import { YesNoPipe } from '@shared/pipes/yes-no.pipe';
import { WeekdayPipe } from '@shared/pipes/weekday.pipe';
import { FullnamePipe } from '@shared/pipes/fullname.pipe';
import { SafePipe } from '@shared/pipes/safe.pipe';
import { AddressPipe } from '@shared/pipes/address.pipe';
import { CareSettingPipe } from '@shared/pipes/care-setting.pipe';
import { JoinPipe } from '@shared/pipes/join.pipe';
import { RolePipe } from '@shared/pipes/role-pipe';
import { InRolePipe } from '@shared/pipes/in-role-pipe';
import { AddressFormComponent } from '@shared/components/forms/address-form/address-form.component';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { ProgressIndicatorComponent } from '@shared/components/progress-indicator/progress-indicator.component';

import { PageComponent } from '@shared/components/pages/page/page.component';
import { PageHeaderComponent } from '@shared/components/pages/page-header/page-header.component';
import { PageSubheaderComponent } from '@shared/components/pages/page-subheader/page-subheader.component';
import { PageSubheaderTitleDirective } from '@shared/components/pages/page-subheader/page-subheader-title.directive';
import { PageSubheaderSummaryDirective } from '@shared/components/pages/page-subheader/page-subheader-summary.directive';
import { PageSubheader2Component } from '@shared/components/pages/page-subheader2/page-subheader2.component';
import { PageSubheader2TitleDirective } from '@shared/components/pages/page-subheader2/page-subheader2-title.directive';
import { PageSubheader2SummaryDirective } from '@shared/components/pages/page-subheader2/page-subheader2-summary.directive';
import { PageSubheader2MoreInfoDirective } from '@shared/components/pages/page-subheader2/page-subheader2-more-info.directive';
import { PageSectionComponent } from './components/pages/page-section/page-section.component';
import { PageFooterComponent } from '@shared/components/pages/page-footer/page-footer.component';

import { NotificationInfoSummaryDirective } from '@shared/components/forms/contact-information-form/notification-info-summary.directive';
import { DialogContentDirective } from '@shared/components/dialogs/dialog-content.directive';
import { FormIconGroupComponent } from '@shared/components/form-icon-group/form-icon-group.component';
import { AlertComponent } from '@shared/components/alerts/alert/alert.component';
import { EnrolleePropertyComponent } from '@shared/components/enrollee/enrollee-property/enrollee-property.component';
import { EnrolleePropertyErrorComponent } from '@shared/components/enrollee/enrollee-property-error/enrollee-property-error.component';
import { EnrolleeReviewComponent } from '@shared/components/enrollee/enrollee-review/enrollee-review.component';
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
import {
  CollegeCertificationFormComponent
} from '@shared/components/forms/college-certification-form/college-certification-form.component';
import { AddressAutocompleteComponent } from '@shared/components/address-autocomplete/address-autocomplete.component';
import { RemoteUserReviewComponent } from '@shared/components/site/remote-user-review/remote-user-review.component';
import { AccessCodeFormComponent } from '@shared/components/forms/access-code-form/access-code-form.component';
import { ContactInformationFormComponent } from '@shared/components/forms/contact-information-form/contact-information-form.component';
import {
  EnrolleeSelfDeclarationsComponent
} from '@shared/components/enrollee/enrollee-self-declarations/enrollee-self-declarations.component';
import { TriageComponent } from '@shared/components/dialogs/content/triage/triage.component';
import { SendEmailComponent } from '@shared/components/dialogs/content/send-email/send-email.component';
import { ToggleContentComponent } from '@shared/components/toggle-content/toggle-content.component';
import { BcscProfileComponent } from '@shared/components/bcsc-profile/bcsc-profile.component';
import { PreferredNameFormComponent } from '@shared/components/forms/preferred-name-form/preferred-name-form.component';
import { EscalationNoteComponent } from '@shared/components/dialogs/content/escalation-note/escalation-note.component';
import { BannerComponent } from '@shared/components/banner/banner.component';
import { ClaimNoteComponent } from '@shared/components/dialogs/content/claim-note/claim-note.component';
import { AddressViewComponent } from '@shared/components/address-view/address-view.component';
import { SiteInformationFormComponent } from '@shared/components/forms/site-information-form/site-information-form.component';
import { SiteRegAccessComponent } from '@shared/components/auth/site-reg-access/site-reg-access.component';
import { PillComponent } from '@shared/components/auth/pill/pill.component';
import { SimpleAccessComponent } from '@shared/components/auth/simple-access/simple-access.component';
import { PrimeEnrolmentAccessComponent } from '@shared/components/auth/prime-enrolment-access/prime-enrolment-access.component';
import { VendorFormComponent } from '@shared/components/forms/vendor-form/vendor-form.component';
import { ContactProfileFormComponent } from '@shared/components/site/contact-profile-form/contact-profile-form.component';
import { SameAsComponent } from '@shared/components/site/same-as/same-as.component';
import { SummaryCardComponent } from '@shared/components/site/summary-card/summary-card.component';
import { SendBulkEmailComponent } from '@shared/components/dialogs/content/send-bulk-email/send-bulk-email.component';
import { PaginatorComponent } from '@shared/components/paginator/paginator.component';
import { DoingBusinessAsFormFieldComponent } from '@shared/components/forms/fields/doing-business-as-form-field/doing-business-as-form-field.component';
import { CardListComponent } from '@shared/components/card-list/card-list.component';
import { OptionsFormComponent } from '@shared/components/forms/options-form/options-form.component';
import { OboSiteFormComponent } from '@shared/components/obo-site-form/obo-site-form.component';
import { ExpiryAlertComponent } from '@shared/components/alerts/expiry-alert/expiry-alert.component';
import { ErrorLoggerComponent } from '@shared/components/dialogs/content/error-logger/error-logger.component';

@NgModule({
  declarations: [
    CapitalizePipe,
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
    RolePipe,
    InRolePipe,
    AddressFormComponent,
    DefaultPipe,
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
    PreferredNameFormComponent,
    ToggleContentComponent,
    BcscProfileComponent,
    PreferredNameFormComponent,
    TriageComponent,
    EscalationNoteComponent,
    ClaimNoteComponent,
    AddressViewComponent,
    PageSectionComponent,
    SiteInformationFormComponent,
    SiteRegAccessComponent,
    PillComponent,
    SimpleAccessComponent,
    PrimeEnrolmentAccessComponent,
    VendorFormComponent,
    ContactProfileFormComponent,
    SameAsComponent,
    SummaryCardComponent,
    BannerComponent,
    SendBulkEmailComponent,
    PaginatorComponent,
    DoingBusinessAsFormFieldComponent,
    CardListComponent,
    OptionsFormComponent,
    OboSiteFormComponent,
    ExpiryAlertComponent,
    ErrorLoggerComponent
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
    DefaultPipe,
    FormatDatePipe,
    PhonePipe,
    PostalPipe,
    ReplacePipe,
    YesNoPipe,
    WeekdayPipe,
    FullnamePipe,
    SafePipe,
    AddressPipe,
    RolePipe,
    InRolePipe,
    AddressFormComponent,
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
    ToggleContentComponent,
    BcscProfileComponent,
    PreferredNameFormComponent,
    TriageComponent,
    EscalationNoteComponent,
    ClaimNoteComponent,
    AddressViewComponent,
    PageSectionComponent,
    SiteInformationFormComponent,
    SiteRegAccessComponent,
    PillComponent,
    SimpleAccessComponent,
    PrimeEnrolmentAccessComponent,
    VendorFormComponent,
    ContactProfileFormComponent,
    SameAsComponent,
    SummaryCardComponent,
    BannerComponent,
    PaginatorComponent,
    DoingBusinessAsFormFieldComponent,
    CardListComponent,
    OptionsFormComponent,
    OboSiteFormComponent,
    ExpiryAlertComponent
  ],
  providers: [
    FullnamePipe,
    AddressPipe,
    CasePipe,
    CapitalizePipe
  ]
})
export class SharedModule {}
