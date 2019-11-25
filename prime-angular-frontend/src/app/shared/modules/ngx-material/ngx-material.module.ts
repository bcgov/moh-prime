import { NgModule } from '@angular/core';
import {
  MatAutocompleteModule, MatButtonModule, MatCheckboxModule, MatChipsModule,
  MatDatepickerModule, MatDialogModule, MatIconModule, MatInputModule,
  MatListModule, MatMenuModule, MatSelectModule, MatSidenavModule,
  MatSlideToggleModule, MatSnackBarModule, MatTableModule, MatToolbarModule,
  MatTooltipModule, MatPaginatorModule, MatRadioModule, DateAdapter,
  MAT_DATE_LOCALE, MAT_DIALOG_DEFAULT_OPTIONS, MAT_DATE_FORMATS,
  MatFormFieldDefaultOptions, MAT_FORM_FIELD_DEFAULT_OPTIONS,
  MAT_LABEL_GLOBAL_OPTIONS
} from '@angular/material';
import { MomentDateAdapter, MatMomentDateModule } from '@angular/material-moment-adapter';

export const APP_DATE_FORMAT = 'D MMM YYYY';
export const APP_DATE_FORMATS = {
  parse: {
    // Reformat entered date values to this format
    dateInput: APP_DATE_FORMAT,
  },
  display: {
    dateInput: APP_DATE_FORMAT,
    monthYearLabel: 'MMM YYYY',
    dateA11yLabel: APP_DATE_FORMAT,
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
      provide: MAT_DATE_FORMATS,
      useValue: APP_DATE_FORMATS
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
      provide: MAT_LABEL_GLOBAL_OPTIONS,
      useValue: { float: 'always' }
    },
    {
      provide: MAT_FORM_FIELD_DEFAULT_OPTIONS,
      useValue: matFormFieldCustomOptions
    }
  ]
})
export class NgxMaterialModule { }
