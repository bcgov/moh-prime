import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';

import { NgxBusyModule } from '@shared/modules/ngx-busy/ngx-busy.module';
import { NgxProgressModule } from '@shared/modules/ngx-progress/ngx-progress.module';
import { NgxMaterialModule } from '@shared/modules/ngx-material/ngx-material.module';
import { ApplicationHeaderComponent } from '@shared/components/application-header/application-header.component';
import { PageComponent } from '@shared/components/page/page.component';
import { PageHeaderComponent } from '@shared/components/page-header/page-header.component';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { DialogContentDirective } from '@shared/components/dialogs/dialog-content.directive';

@NgModule({
  declarations: [
    ApplicationHeaderComponent,
    PageComponent,
    PageHeaderComponent,
    ConfirmDialogComponent,
    DialogContentDirective
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
    ApplicationHeaderComponent,
    PageComponent,
    PageHeaderComponent,
    DialogContentDirective
  ],
  entryComponents: [
    ConfirmDialogComponent
  ]
})
export class SharedModule { }
