import { NgModule } from '@angular/core';
import {
  MatAutocompleteModule, MatButtonModule, MatCheckboxModule, MatChipsModule,
  MatDatepickerModule, MatDialogModule, MatIconModule, MatInputModule,
  MatListModule, MatMenuModule, MatSelectModule, MatSidenavModule,
  MatSlideToggleModule, MatSnackBarModule, MatTableModule, MatToolbarModule,
  MatTooltipModule, MatPaginatorModule, MatRadioModule,
  DateAdapter, MAT_DATE_LOCALE, MAT_DIALOG_DEFAULT_OPTIONS,
  MatFormFieldDefaultOptions, MAT_FORM_FIELD_DEFAULT_OPTIONS, MAT_DATE_FORMATS
} from '@angular/material';
import { MomentDateAdapter, MatMomentDateModule } from '@angular/material-moment-adapter';

export const APP_DATE_FORMATS = {
  parse: {
    // Reformat entered date values to this format
    dateInput: 'D MMM YYYY',
  },
  display: {
    dateInput: 'D MMM YYYY',
    monthYearLabel: 'MMM YYYY',
    dateA11yLabel: 'D MMM YYYY',
    monthYearA11yLabel: 'MMM YYYY',
  }
};

const matFormFieldCustomOptions: MatFormFieldDefaultOptions = {
  hideRequiredMarker: true
};

@NgModule({
  exports: [
    MatAutocompleteModule,
    MatButtonModule,
    MatCheckboxModule,
    MatChipsModule,
    MatDatepickerModule,
    MatDialogModule,
    MatIconModule,
    MatInputModule,
    MatListModule,
    MatMenuModule,
    MatMomentDateModule,
    MatSelectModule,
    MatSidenavModule,
    MatSlideToggleModule,
    MatSnackBarModule,
    MatTableModule,
    MatToolbarModule,
    MatTooltipModule,
    MatPaginatorModule,
    MatRadioModule
  ],
  providers: [
    {
      provide: MAT_DATE_FORMATS, useValue: APP_DATE_FORMATS
    },
    {
      provide: DateAdapter,
      useClass: MomentDateAdapter,
      deps: [MAT_DATE_LOCALE]
    },
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
