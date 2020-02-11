import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

import { NgxMaskModule } from 'ngx-mask';
import { ClipboardModule } from 'ngx-clipboard';
import { NgxBusyModule } from './modules/ngx-busy/ngx-busy.module';
import { MarkdownModule } from 'ngx-markdown';

import { ConfigModule } from '@config/config.module';
import { NgxContextualHelpModule } from '@shared/modules/ngx-contextual-help/ngx-contextual-help.module';
import { NgxProgressModule } from '@shared/modules/ngx-progress/ngx-progress.module';
import { NgxMaterialModule } from '@shared/modules/ngx-material/ngx-material.module';
import { CapitalizePipe } from '@shared/pipes/capitalize.pipe';
import { AddressComponent } from '@shared/components/forms/address/address.component';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import {
  EnrolmentStatusReasonsComponent
} from '@shared/components/dialogs/content/enrolment-status-reasons/enrolment-status-reasons.component';
import { EnrolleePipe } from '@shared/pipes/enrollee.pipe';
import { EnrolmentPipe } from '@shared/pipes/enrolment.pipe';
import { FirstKeyPipe } from '@shared/pipes/first-key.pipe';
import { FormatDatePipe } from '@shared/pipes/format-date.pipe';
import { PhonePipe } from '@shared/pipes/phone.pipe';
import { PostalPipe } from '@shared/pipes/postal.pipe';
import { ReplacePipe } from '@shared/pipes/replace.pipe';
import { CertificatePipe } from '@shared/pipes/certificate.pipe';
import { DefaultPipe } from '@shared/pipes/default.pipe';
import { YesNoPipe } from '@shared/pipes/yes-no.pipe';
import { DashboardComponent } from '@shared/components/dashboard/dashboard.component';
import { HeaderComponent } from '@shared/components/header/header.component';
import { PageComponent } from '@shared/components/page/page.component';
import { PageHeaderComponent } from '@shared/components/page-header/page-header.component';
import { PageSubheaderComponent } from './components/page-subheader/page-subheader.component';
import { PageSubheaderTitleDirective } from '@shared/components/page-subheader/page-subheader-title.directive';
import { PageSubheaderSummaryDirective } from '@shared/components/page-subheader/page-subheader-summary.directive';
import { DialogContentDirective } from '@shared/components/dialogs/dialog-content.directive';
import { ClipboardIconComponent } from '@shared/components/clipboard-icon/clipboard-icon.component';
import { FormIconGroupComponent } from '@shared/components/form-icon-group/form-icon-group.component';
import { AlertComponent } from '@shared/components/alert/alert.component';
import { ProgressIndicatorComponent } from '@shared/components/progress-indicator/progress-indicator.component';
import { EnrolleePropertyComponent } from '@shared/components/enrollee/enrollee-property/enrollee-property.component';
import { EnrolleeReviewComponent } from '@shared/components/enrollee/enrollee-review/enrollee-review.component';
import { EnrolleeProfileComponent } from '@shared/components/enrollee/enrollee-profile/enrollee-profile.component';
import { EnrolleeAddressComponent } from '@shared/components/enrollee/enrollee-address/enrollee-address.component';
import { EnrolleePrivilegesComponent } from '@shared/components/enrollee/enrollee-privileges/enrollee-privileges.component';
import { EnrolleeOrganizationsComponent } from '@shared/components/enrollee/enrollee-organizations/enrollee-organizations.component';
import { PrimeEmailComponent } from './components/prime-email/prime-email.component';
import { PrimePhoneComponent } from './components/prime-phone/prime-phone.component';
import { ApproveEnrolmentComponent } from './components/dialogs/content/approve-enrolment/approve-enrolment.component';
import { SafeHtmlPipe } from './pipes/safe-html.pipe';
import { MarkdownComponent } from './components/dialogs/content/markdown/markdown.component';

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
    DialogContentDirective,
    FormIconGroupComponent,
    AlertComponent,
    ClipboardIconComponent,
    EnrolleeReviewComponent,
    CertificatePipe,
    ProgressIndicatorComponent,
    EnrolleeProfileComponent,
    EnrolleeAddressComponent,
    EnrolleePrivilegesComponent,
    EnrolleeOrganizationsComponent,
    EnrolleePropertyComponent,
    PrimeEmailComponent,
    PrimePhoneComponent,
    ApproveEnrolmentComponent,
    SafeHtmlPipe,
    MarkdownComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    ClipboardModule,
    ConfigModule,
    NgxBusyModule,
    NgxContextualHelpModule,
    NgxMaskModule.forRoot(),
    NgxMaterialModule,
    NgxProgressModule,
    ReactiveFormsModule,
    MarkdownModule.forRoot()
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
    AddressComponent,
    DashboardComponent,
    EnrolmentStatusReasonsComponent,
    HeaderComponent,
    PageComponent,
    PageHeaderComponent,
    PageSubheaderComponent,
    PageSubheaderTitleDirective,
    PageSubheaderSummaryDirective,
    DialogContentDirective,
    FormIconGroupComponent,
    AlertComponent,
    ClipboardIconComponent,
    EnrolleeReviewComponent,
    CertificatePipe,
    ProgressIndicatorComponent,
    EnrolleeProfileComponent,
    EnrolleeAddressComponent,
    EnrolleePrivilegesComponent,
    EnrolleeOrganizationsComponent,
    EnrolleePropertyComponent,
    PrimeEmailComponent,
    PrimePhoneComponent,
    SafeHtmlPipe
  ],
  entryComponents: [
    ConfirmDialogComponent,
    EnrolmentStatusReasonsComponent,
    ApproveEnrolmentComponent,
    MarkdownComponent
  ]
})
export class SharedModule { }
