import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';

import { NgxMaterialModule } from '@shared/modules/ngx-material/ngx-material.module';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule,
    ReactiveFormsModule,
    NgxMaterialModule
  ],
  exports: [
    CommonModule,
    ReactiveFormsModule,
    NgxMaterialModule
  ]
})
export class SharedModule { }
