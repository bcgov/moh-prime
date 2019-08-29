import { Injectable } from '@angular/core';

import { MatSnackBar, MatSnackBarConfig } from '@angular/material';

@Injectable({
  providedIn: 'root'
})
export class ToastService {
  private duration: number;

  constructor(
    private snackBar: MatSnackBar
  ) {
    this.duration = 8000; // ms
  }

  public openSuccessToast(message: string, action: string = null, config: MatSnackBarConfig = null) {
    const defaultConfig: MatSnackBarConfig = Object.assign({
      duration: this.duration,
      extraClasses: ['success']
    }, config);
    this.openToast(message, action, defaultConfig);
  }

  public openErrorToast(message: string, action: string = null, config: MatSnackBarConfig = null) {
    const defaultConfig: MatSnackBarConfig = Object.assign({
      duration: this.duration,
      extraClasses: ['danger']
    }, config);
    this.openToast(message, action, defaultConfig);
  }

  private openToast(message: string, action: string = null, config: MatSnackBarConfig) {
    this.snackBar.open(message, action, config);
  }
}
