import { Injectable } from '@angular/core';

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

  /**
   * Opens a toast to display success messages.
   *
   * @param {string} message
   * @param {string} [action=null]
   * @param {MatSnackBarConfig} [config=null]
   * @memberof ToastService
   */
  public openSuccessToast(message: string, action: string = null, config: MatSnackBarConfig = null) {
    const defaultConfig: MatSnackBarConfig = Object.assign({
      duration: this.duration,
      extraClasses: ['success']
    }, config);
    this.openToast(message, action, defaultConfig);
  }

  /**
   * Opens a toast to display error messages.
   *
   * @param {string} message
   * @param {string} [action=null]
   * @param {MatSnackBarConfig} [config=null]
   * @memberof ToastService
   */
  public openErrorToast(message: string, action: string = null, config: MatSnackBarConfig = null) {
    const defaultConfig: MatSnackBarConfig = Object.assign({
      duration: this.duration,
      extraClasses: ['danger']
    }, config);
    this.openToast(message, action, defaultConfig);
  }

  /**
   * Opens a toast.
   *
   * @private
   * @param {string} message
   * @param {string} [action=null]
   * @param {MatSnackBarConfig} config
   * @memberof ToastService
   */
  private openToast(message: string, action: string = null, config: MatSnackBarConfig) {
    this.snackBar.open(message, action, config);
  }
}
