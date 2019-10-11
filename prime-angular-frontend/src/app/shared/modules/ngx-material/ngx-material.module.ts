import { NgModule } from '@angular/core';
import {
  MatInputModule, MatSelectModule, MatCheckboxModule, MatRadioModule,
  MatButtonModule, MatSnackBarModule, MatIconModule, MatDialogModule,
  MatSidenavModule, MatDatepickerModule, DateAdapter, MAT_DATE_LOCALE,
  MatChipsModule, MatAutocompleteModule, MatSlideToggleModule,
  MAT_DIALOG_DEFAULT_OPTIONS, MatToolbarModule, MatMenuModule, MatListModule,
  MatTooltipModule
} from '@angular/material';
import { MatMomentDateModule, MomentDateAdapter } from '@angular/material-moment-adapter';

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
  imports: [
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
    MatToolbarModule,
    MatTooltipModule,
    MatRadioModule
  ],
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
    MatToolbarModule,
    MatTooltipModule,
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
