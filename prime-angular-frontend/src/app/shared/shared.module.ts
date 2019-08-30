import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
// import { ReactiveFormsModule } from '@angular/forms';

import { NgxMaterialModule } from './modules/ngx-material/ngx-material.module';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    NgxMaterialModule,
    // ReactiveFormsModule
  ],
  exports: [
    CommonModule,
    NgxMaterialModule,
    // ReactiveFormsModule
  ]
})
export class SharedModule { }
