import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';

import { environment } from '@env/environment';
import { AbstractLogger } from '@shared/classes/abstract-logger';
import { ConsoleLogger } from '@shared/classes/console-logger';
import { DialogLogger } from '@shared/classes/dialog-logger';

@Injectable({
  providedIn: 'root'
})
export class LoggerService {
  private loggers: AbstractLogger[] = [];

  constructor(
    private dialog: MatDialog,
    private snackBar: MatSnackBar
  ) {
    // TODO: use configs to add loggers
    this.loggers.push(new DialogLogger(this.dialog, this.snackBar));
    this.loggers.push(new ConsoleLogger());
    // If there is no logger configured, always default to the console logger
    if (this.loggers.length == 0) {
      this.loggers.push(new ConsoleLogger());
    }
  }

  /**
   * @description
   * General output of logging information.
   */
  public log(msg: string, ...data: any[]) {
    this.print('log', { msg, data });
  }

  /**
   * @description
   * Informative output of logging information.
   */
  public info(msg: string, ...data: any[]) {
    this.print('info', { msg, data });
  }

  /**
   * @description
   * Outputs a warning message.
   */
  public warn(msg: string, ...data: any[]) {
    this.print('warn', { msg, data });
  }

  /**
   * @description
   * Outputs an error message.
   */
  public error(msg: string, ...data: any[]) {
    this.print('error', { msg, data });
  }

  /**
   * @description
   * Outputs a stack trace.
   */
  public trace(msg: string, ...data: any[]) {
    this.print('error', { msg, data });
  }

  /**
   * @description
   * Pretty print JSON.
   */
  public pretty(msg: string, ...data: any[]) {
    this.print('log', { msg, data: [JSON.stringify(data, null, '\t')] });
  }

  /**
   * @description
   * Prints the logging information, but ONLY if not in production.
   */
  private print(type: string, params: { msg?: string, data?: any[] }) {
    for (let logger of this.loggers) {
      logger.log(type, params).subscribe(result => { });
    }
  }
}
