import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import { BusyModule } from '@shared/modules/busy/busy.module';
import { NgxMaterialModule } from '@shared/modules/ngx-material/ngx-material.module';
import { NgxBootstrapModule } from '@shared/modules/ngx-bootstrap/ngx-bootstrap.module';
import { CapitalizePipe } from '@shared/pipes/capitalize.pipe';
import { PhonePipe } from '@shared/pipes/phone.pipe';
import { ReplacePipe } from '@shared/pipes/replace.pipe';
import { FirstKeyPipe } from './pipes/first-key.pipe';

@NgModule({
  declarations: [
    CapitalizePipe,
    PhonePipe,
    ReplacePipe,
    FirstKeyPipe,
    FormFieldValidityDirective
  ],
  imports: [
    BusyModule,
    CommonModule,
    NgxBootstrapModule,
    NgxMaterialModule,
    ReactiveFormsModule,
  ],
  exports: [
    BusyModule,
    CommonModule,
    NgxBootstrapModule,
    NgxMaterialModule,
    ReactiveFormsModule,
    CapitalizePipe,
    PhonePipe,
    ReplacePipe,
    FirstKeyPipe,
  ]
})
export class SharedModule { }
