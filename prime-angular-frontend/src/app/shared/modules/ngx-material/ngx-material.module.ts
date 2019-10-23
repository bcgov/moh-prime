import { NgModule } from '@angular/core';
import {
  MatAutocompleteModule, MatButtonModule, MatCheckboxModule, MatChipsModule,
  MatDatepickerModule, MatDialogModule, MatIconModule, MatInputModule,
  MatListModule, MatMenuModule, MatSelectModule, MatSidenavModule,
  MatSlideToggleModule, MatSnackBarModule, MatTableModule, MatToolbarModule,
  MatTooltipModule, MatPaginatorModule, MatRadioModule,
  DateAdapter, MAT_DATE_LOCALE, MAT_DIALOG_DEFAULT_OPTIONS,
} from '@angular/material';
import { MomentDateAdapter, MatMomentDateModule } from '@angular/material-moment-adapter';

export const APP_DATE_FORMATS = {
  parse: {
    dateInput: 'LL',
  },
  display: {
    dateInput: 'LL',
    monthYearLabel: 'MMM YYYY',
    dateA11yLabel: 'LL',
    monthYearA11yLabel: 'MMMM YYYY',
  }
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
      provide: DateAdapter,
      useClass: MomentDateAdapter,
      deps: [MAT_DATE_LOCALE]
    },
    {
      provide: MAT_DIALOG_DEFAULT_OPTIONS, useValue: {
        width: '500px',
        hasBackdrop: true
      }
    },
  ]
})
export class NgxMaterialModule { }
