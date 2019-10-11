import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import { NgxMaskModule } from 'ngx-mask';

import { ConfigModule } from '@config/config.module';
import { BusyModule } from '@shared/modules/busy/busy.module';
import { NgxProgressModule } from '@shared/modules/ngx-progress/ngx-progress.module';
import { NgxMaterialModule } from '@shared/modules/ngx-material/ngx-material.module';
import { NgxBootstrapModule } from '@shared/modules/ngx-bootstrap/ngx-bootstrap.module';
import { CapitalizePipe } from '@shared/pipes/capitalize.pipe';
import { PhonePipe } from '@shared/pipes/phone.pipe';
import { ReplacePipe } from '@shared/pipes/replace.pipe';
import { FirstKeyPipe } from '@shared/pipes/first-key.pipe';
import { FormControlValidityDirective } from '@shared/directives/form-control-validity.directive';
import { HeaderComponent } from '@shared/components/header/header.component';
import { ConfirmDiscardChangesDialogComponent } from '@shared/components/dialogs/confirm-discard-changes-dialog/confirm-discard-changes-dialog.component';

@NgModule({
  declarations: [
    CapitalizePipe,
    PhonePipe,
    ReplacePipe,
    FirstKeyPipe,
    FormControlValidityDirective,
    ConfirmDiscardChangesDialogComponent,
    HeaderComponent
  ],
  imports: [
    BusyModule,
    CommonModule,
    NgxBootstrapModule,
    NgxMaterialModule,
    ReactiveFormsModule,
    NgxMaskModule.forRoot(),
    NgxProgressModule
  ],
  exports: [
    BusyModule,
    CommonModule,
    ConfigModule,
    NgxMaterialModule,
    NgxProgressModule,
    NgxBootstrapModule,
    NgxMaskModule,
    ReactiveFormsModule,
    CapitalizePipe,
    PhonePipe,
    ReplacePipe,
    FirstKeyPipe,
    FormControlValidityDirective,
    HeaderComponent
  ],
  entryComponents: [
    ConfirmDiscardChangesDialogComponent
  ]
})
export class SharedModule { }
