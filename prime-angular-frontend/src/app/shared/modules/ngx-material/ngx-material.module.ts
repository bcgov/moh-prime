import { NgModule } from '@angular/core';
import {
  MatSidenavModule, MatDialogModule, MatButtonModule, MatSnackBarModule,
  MatToolbarModule, MatIconModule, MatListModule, MatDividerModule,
  MatChipsModule, MatMenuModule, MatCardModule, MatDatepickerModule,
  MatProgressSpinnerModule, MatProgressBarModule, MAT_DIALOG_DEFAULT_OPTIONS, MAT_DATE_FORMATS, MatSlideToggleModule,
} from '@angular/material';
// import { MatMomentDateModule } from '@angular/material-moment-adapter';
import { CdkTableModule } from '@angular/cdk/table';
import { CdkStepperModule } from '@angular/cdk/stepper';

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
    MatSidenavModule,
    MatDialogModule,
    MatButtonModule,
    MatSnackBarModule,
    MatToolbarModule,
    MatIconModule,
    MatListModule,
    MatDatepickerModule,
    MatDividerModule,
    MatChipsModule,
    MatMenuModule,
    MatCardModule,
    MatSlideToggleModule,
    // MatMomentDateModule,
    MatProgressSpinnerModule,
    MatProgressBarModule,
    CdkTableModule,
    CdkStepperModule
  ],
  exports: [
    MatSidenavModule,
    MatDialogModule,
    MatButtonModule,
    MatSnackBarModule,
    MatToolbarModule,
    MatIconModule,
    MatListModule,
    MatDatepickerModule,
    MatDividerModule,
    MatChipsModule,
    MatMenuModule,
    MatCardModule,
    MatSlideToggleModule,
    // MatMomentDateModule,
    MatProgressSpinnerModule,
    MatProgressBarModule,
    CdkTableModule,
    CdkStepperModule
  ],
  providers: [
    {
      provide: MAT_DIALOG_DEFAULT_OPTIONS, useValue: {
        width: '500px',
        hasBackdrop: true
      }
    },
    {
      provide: MAT_DATE_FORMATS, useValue: APP_DATE_FORMATS
    }
  ]
})
export class NgxMaterialModule { }
