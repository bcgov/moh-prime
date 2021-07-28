import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';

import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { ErrorLoggerComponent } from '@shared/components/dialogs/content/error-logger/error-logger.component';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';

@Injectable({
  providedIn: 'root'
})
export class DialogLogger {
  constructor(
    private dialog: MatDialog
  ) { }

  public log(logId: number, error: Error): void {
    const data: DialogOptions = {
      icon: 'error',
      actionType: 'warn',
      title: `Error Occurred (ID: ${logId})`,
      data: error,
      actionText: 'OK',
      cancelHide: true,
      component: ErrorLoggerComponent
    };

    this.dialog.open(ConfirmDialogComponent, { data })
      .afterClosed()
      .subscribe();
  }
}
