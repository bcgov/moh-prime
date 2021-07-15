import { Injectable } from '@angular/core';

import { environment } from '@env/environment';

@Injectable({
  providedIn: 'root'
})
export class LoggerService {
  constructor() { }

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
    if (!environment.production || type === 'error' || type === 'warn') {

      const message = this.colorize(type, params.msg);

      if (params.msg && params.data.length) {
        console[type](...message, ...params.data);
      } else if (!params.msg && params.data.length) {
        console[type](params.data);
      } else if (params.msg && !params.data.length) {
        console[type](...message);
      } else {
        console.error('Logger parameters are invalid: ', params);
      }
    }
  }

  /**
   * @description
   * Apply colour to the console message, otherwise the use
   * the default.
   */
  private colorize(type: string, msg: string): string[] {
    let color = '';

    switch (type) {
      case 'log':
        color = 'Yellow';
        break;
      case 'info':
        color = 'DodgerBlue';
        break;
      case 'error':
        color = 'Red';
        break;
      case 'warning':
        color = 'Orange';
        break;
    }

    if (color) {
      color = `color:${color}`;
    }

    return [`%c${msg}`, color];
  }
}
