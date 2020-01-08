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
    this.duration = 2000; // ms
  }

  /**
   * @description
   * Opens a toast to display success messages.
   */
  public openSuccessToast(message: string, action: string = null, config: MatSnackBarConfig = null) {
    const defaultConfig: MatSnackBarConfig = Object.assign({
      duration: this.duration,
      extraClasses: ['success']
    }, config);
    this.openToast(message, action, defaultConfig);
  }

  /**
   * @description
   * Opens a toast to display error messages.
   */
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
