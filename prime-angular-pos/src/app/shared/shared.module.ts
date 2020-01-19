import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';

import { NgxBusyModule } from '@shared/modules/ngx-busy/ngx-busy.module';
import { NgxProgressModule } from '@shared/modules/ngx-progress/ngx-progress.module';
import { NgxMaterialModule } from '@shared/modules/ngx-material/ngx-material.module';
import { HeaderComponent } from '@shared/components/header/header.component';
import { PageComponent } from '@shared/components/page/page.component';
import { PageHeaderComponent } from '@shared/components/page-header/page-header.component';

@NgModule({
  declarations: [
    HeaderComponent,
    PageComponent,
    PageHeaderComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    ReactiveFormsModule,
    NgxBusyModule,
    NgxMaterialModule,
    NgxProgressModule
  ],
  exports: [
    CommonModule,
    ReactiveFormsModule,
    NgxBusyModule,
    NgxMaterialModule,
    NgxProgressModule,
    HeaderComponent,
    PageComponent,
    PageHeaderComponent
  ]
})
export class SharedModule { }
