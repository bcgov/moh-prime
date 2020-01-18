import { NgModule } from '@angular/core';
import {
  MatDialogModule, MatIconModule, MatFormFieldDefaultOptions,
  MatButtonModule,
  MAT_FORM_FIELD_DEFAULT_OPTIONS, MAT_DIALOG_DEFAULT_OPTIONS
} from '@angular/material';

const matFormFieldCustomOptions: MatFormFieldDefaultOptions = {
  hideRequiredMarker: true
};

@NgModule({
  exports: [
    MatButtonModule,
    MatDialogModule,
    MatIconModule
  ],
  providers: [
    {
      provide: MAT_DIALOG_DEFAULT_OPTIONS,
      useValue: {
        width: '500px',
        hasBackdrop: true
      }
    },
    {
      provide: MAT_FORM_FIELD_DEFAULT_OPTIONS,
      useValue: matFormFieldCustomOptions
    }
  ]
})
export class NgxMaterialModule { }
