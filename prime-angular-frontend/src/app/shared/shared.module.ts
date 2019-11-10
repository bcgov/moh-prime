import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

import { NgxMaskModule } from 'ngx-mask';

import { ConfigModule } from '@config/config.module';
import { NgxProgressModule } from '@shared/modules/ngx-progress/ngx-progress.module';
import { NgxMaterialModule } from '@shared/modules/ngx-material/ngx-material.module';
import { CapitalizePipe } from '@shared/pipes/capitalize.pipe';
import { AddressComponent } from '@shared/components/forms/address/address.component';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { EnrolmentStatusReasonsComponent } from '@shared/components/dialogs/content/enrolment-status-reasons/enrolment-status-reasons.component';
import { EnrolmentPipe } from '@shared/pipes/enrolment.pipe';
import { FirstKeyPipe } from '@shared/pipes/first-key.pipe';
import { FormatDatePipe } from '@shared/pipes/format-date.pipe';
import { PhonePipe } from '@shared/pipes/phone.pipe';
import { PostalPipe } from '@shared/pipes/postal.pipe';
import { ReplacePipe } from '@shared/pipes/replace.pipe';
import { DashboardComponent } from '@shared/components/dashboard/dashboard.component';
import { HeaderComponent } from '@shared/components/header/header.component';
import { SubHeaderComponent } from '@shared/components/sub-header/sub-header.component';
import { DialogContentDirective } from './components/dialogs/dialog-content.directive';

@NgModule({
  declarations: [
    CapitalizePipe,
    PhonePipe,
    ReplacePipe,
    FirstKeyPipe,
    FormatDatePipe,
    PostalPipe,
    EnrolmentPipe,
    AddressComponent,
    DashboardComponent,
    ConfirmDialogComponent,
    EnrolmentStatusReasonsComponent,
    HeaderComponent,
    SubHeaderComponent,
    DialogContentDirective
  ],
  imports: [
    CommonModule,
    RouterModule,
    NgxMaskModule.forRoot(),
    NgxMaterialModule,
    NgxProgressModule,
    ReactiveFormsModule
  ],
  exports: [
    CommonModule,
    ConfigModule,
    NgxMaterialModule,
    NgxProgressModule,
    NgxMaskModule,
    ReactiveFormsModule,
    CapitalizePipe,
    EnrolmentPipe,
    FirstKeyPipe,
    FormatDatePipe,
    PhonePipe,
    PostalPipe,
    ReplacePipe,
    AddressComponent,
    DashboardComponent,
    EnrolmentStatusReasonsComponent,
    HeaderComponent,
    SubHeaderComponent,
    DialogContentDirective
  ],
  entryComponents: [
    ConfirmDialogComponent,
    EnrolmentStatusReasonsComponent
  ]
})
export class SharedModule { }
