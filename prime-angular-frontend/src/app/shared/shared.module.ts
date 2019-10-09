import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import { NgxMaskModule, IConfig } from 'ngx-mask';

import { BusyModule } from '@shared/modules/busy/busy.module';
import { NgxMaterialModule } from '@shared/modules/ngx-material/ngx-material.module';
import { NgxBootstrapModule } from '@shared/modules/ngx-bootstrap/ngx-bootstrap.module';
import { CapitalizePipe } from '@shared/pipes/capitalize.pipe';
import { PhonePipe } from '@shared/pipes/phone.pipe';
import { ReplacePipe } from '@shared/pipes/replace.pipe';
import { FirstKeyPipe } from '@shared/pipes/first-key.pipe';
import { FormControlValidityDirective } from '@shared/directives/form-control-validity.directive';
import { ConfirmDiscardChangesDialogComponent } from '@shared/components/dialogs/confirm-discard-changes-dialog/confirm-discard-changes-dialog.component';
import { HeaderComponent } from './components/header/header.component';

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
  ],
  exports: [
    BusyModule,
    CommonModule,
    NgxBootstrapModule,
    NgxMaterialModule,
    ReactiveFormsModule,
    NgxMaskModule,
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
