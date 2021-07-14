import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';

import { Observable, of } from 'rxjs';

import { ErrorDialogComponent } from '@shared/components/dialogs/content/error-dialog/error-dialog.component';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';

import { AbstractLogger } from './abstract-logger';

interface LogEntry {
  title: string;
  detail: any[];
}

export class DialogLogger extends AbstractLogger {
  public logs: LogEntry[];

  constructor(
    private dialog: MatDialog,
    private snackBar: MatSnackBar
  ) {
    super();
    this.logs = [];
  }

  public log(type: string, params: { msg?: string; data?: any[]; }): Observable<boolean> {
    // only log errors
    if (type === 'error') {
      const logEntry: LogEntry = { title: params.msg, detail: params.data };
      this.logs.unshift(logEntry);

      this.snackBar.open('Error(s) occurred', 'Open')
        .afterDismissed().subscribe(info => {
          if (info.dismissedByAction === true) {
            const data: DialogOptions = {
              icon: 'error',
              actionType: 'warn',
              title: 'Error(s) occurred',
              data: this.logs,
              actionText: 'OK',
              cancelText: 'Clear'
            };

            this.dialog.open(ErrorDialogComponent, { data })
              .afterClosed()
              .subscribe((result: boolean) => {
                if (result) {
                  // TODO: OK button clicked
                }
                // clear button clicked
                this.logs = [];
              });
          }
        });

      return of(true)
    }

    return of(false);
  }
}
