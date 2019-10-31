import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

import { NgxMaskModule } from 'ngx-mask';

import { ConfigModule } from '@config/config.module';
import { NgxProgressModule } from '@shared/modules/ngx-progress/ngx-progress.module';
import { NgxMaterialModule } from '@shared/modules/ngx-material/ngx-material.module';
import { CapitalizePipe } from '@shared/pipes/capitalize.pipe';
import { PhonePipe } from '@shared/pipes/phone.pipe';
import { ReplacePipe } from '@shared/pipes/replace.pipe';
import { FirstKeyPipe } from '@shared/pipes/first-key.pipe';
import { FormatDatePipe } from '@shared/pipes/format-date.pipe';
import { FormControlValidityDirective } from '@shared/directives/form-control-validity.directive';
import { DashboardComponent } from '@shared/components/dashboard/dashboard.component';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { HeaderComponent } from '@shared/components/header/header.component';
import { SubHeaderComponent } from '@shared/components/sub-header/sub-header.component';
import { PostalPipe } from './pipes/postal.pipe';
import { EnrolmentPipe } from './pipes/enrolment.pipe';

@NgModule({
  declarations: [
    CapitalizePipe,
    PhonePipe,
    ReplacePipe,
    FirstKeyPipe,
    FormControlValidityDirective,
    DashboardComponent,
    ConfirmDialogComponent,
    HeaderComponent,
    SubHeaderComponent,
    FormatDatePipe,
    PostalPipe
    EnrolmentPipe
  ],
  imports: [
    CommonModule,
    RouterModule,
    NgxMaterialModule,
    NgxMaskModule.forRoot(),
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
    PhonePipe,
    ReplacePipe,
    FirstKeyPipe,
    FormatDatePipe,
    EnrolmentPipe,
    FormControlValidityDirective,
    DashboardComponent,
    HeaderComponent,
    SubHeaderComponent,
    PostalPipe
  ],
  entryComponents: [
    ConfirmDialogComponent
  ]
})
export class SharedModule { }
