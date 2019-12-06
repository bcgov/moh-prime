import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

import { NgxMaskModule } from 'ngx-mask';
import { ClipboardModule } from 'ngx-clipboard';
import { NgxBusyModule } from './modules/ngx-busy/ngx-busy.module';

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
import { EnrolmentPipe } from '@shared/pipes/enrolment.pipe';
import { FirstKeyPipe } from '@shared/pipes/first-key.pipe';
import { FormatDatePipe } from '@shared/pipes/format-date.pipe';
import { PhonePipe } from '@shared/pipes/phone.pipe';
import { PostalPipe } from '@shared/pipes/postal.pipe';
import { ReplacePipe } from '@shared/pipes/replace.pipe';
import { CertificatePipe } from '@shared/pipes/certificate.pipe';
import { DefaultPipe } from '@shared/pipes/default.pipe';
import { DashboardComponent } from '@shared/components/dashboard/dashboard.component';
import { HeaderComponent } from '@shared/components/header/header.component';
import { PageHeaderComponent } from '@shared/components/page-header/page-header.component';
import { PageSubheaderComponent } from './components/page-subheader/page-subheader.component';
import { PageSubheaderTitleDirective } from '@shared/components/page-subheader/page-subheader-title.directive';
import { PageSubheaderSummaryDirective } from '@shared/components/page-subheader/page-subheader-summary.directive';
import { DialogContentDirective } from '@shared/components/dialogs/dialog-content.directive';
import { ClipboardIconComponent } from './components/clipboard-icon/clipboard-icon.component';
import { FormIconGroupComponent } from '@shared/components/form-icon-group/form-icon-group.component';
import { AlertComponent } from '@shared/components/alert/alert.component';
import { EnrolleeReviewComponent } from '@shared/components/enrollee-review/enrollee-review.component';

@NgModule({
  declarations: [
    CapitalizePipe,
    PhonePipe,
    ReplacePipe,
    FirstKeyPipe,
    FormatDatePipe,
    PostalPipe,
    EnrolmentPipe,
    DefaultPipe,
    CertificatePipe,
    AddressComponent,
    DashboardComponent,
    ConfirmDialogComponent,
    EnrolmentStatusReasonsComponent,
    HeaderComponent,
    PageHeaderComponent,
    PageSubheaderComponent,
    PageSubheaderTitleDirective,
    PageSubheaderSummaryDirective,
    DialogContentDirective,
    FormIconGroupComponent,
    AlertComponent,
    ClipboardIconComponent,
    EnrolleeReviewComponent
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
    EnrolmentPipe,
    FirstKeyPipe,
    FormatDatePipe,
    PhonePipe,
    PostalPipe,
    ReplacePipe,
    DefaultPipe,
    CertificatePipe,
    AddressComponent,
    DashboardComponent,
    EnrolmentStatusReasonsComponent,
    HeaderComponent,
    PageHeaderComponent,
    PageSubheaderComponent,
    PageSubheaderTitleDirective,
    PageSubheaderSummaryDirective,
    DialogContentDirective,
    FormIconGroupComponent,
    AlertComponent,
    ClipboardIconComponent,
    EnrolleeReviewComponent
  ],
  entryComponents: [
    ConfirmDialogComponent,
    EnrolmentStatusReasonsComponent
  ]
})
export class SharedModule { }
