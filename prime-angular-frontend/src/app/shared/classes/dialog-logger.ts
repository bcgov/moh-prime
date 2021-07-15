import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';

import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';

@Injectable({
  providedIn: 'root'
})
export class DialogLogger {
  constructor(
    private dialog: MatDialog
  ) { }

  public log(message: string, detail: string): void {

    const data: DialogOptions = {
      icon: 'error',
      actionType: 'warn',
      title: message,
      message: detail,
      actionText: 'OK',
      cancelHide: true
    };

    this.dialog.open(ConfirmDialogComponent, { data })
      .afterClosed()
      .subscribe((result: boolean) => {
        if (result) {
          // TODO: OK button clicked
        }
      });
  }
}
