import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { NgxMaterialModule } from './modules/ngx-material/ngx-material.module';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    NgxMaterialModule
  ],
  exports: [
    CommonModule,
    NgxMaterialModule
  ]
})
export class SharedModule { }
