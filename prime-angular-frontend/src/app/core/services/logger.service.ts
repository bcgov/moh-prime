import { Injectable } from '@angular/core';

import { environment } from '@env/environment';

@Injectable({
  providedIn: 'root'
})
export class LoggerService {
  constructor() { }

  /**
   * General output of logging information.
   *
   * @param {string} msg
   * @param {...any[]} data
   * @memberof LoggerService
   */
  public log(msg: string, ...data: any[]) {
    this.print('log', { msg, data });
  }

  /**
   * Informative output of logging information.
   *
   * @param {string} msg
   * @param {...any[]} data
   * @memberof LoggerService
   */
  public info(msg: string, ...data: any[]) {
    this.print('info', { msg, data });
  }

  /**
   * Outputs a warning message.
   *
   * @param {string} msg
   * @param {...any[]} data
   * @memberof LoggerService
   */
  public warn(msg: string, ...data: any[]) {
    this.print('warn', { msg, data });
  }

  /**
   * Outputs an error message.
   *
   * @param {string} msg
   * @param {...any[]} data
   * @memberof LoggerService
   */
  public error(msg: string, ...data: any[]) {
    this.print('error', { msg, data });
  }

  /**
   * Pretty print JSON.
   *
   * @param {string} msg
   * @param {*} data
   * @memberof LoggerService
   */
  public pretty(msg: string, ...data: any[]) {
    this.print('log', { msg, data: [JSON.stringify(data, null, '\t')] });
  }

  /**
   * Prints the logging information, but ONLY if not in production.
   *
   * @private
   * @param {string} type
   * @param {({ msg: string, data?: any[] | any })} params
   * @memberof LoggerService
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
   * Apply colour to the console message, otherwise the use
   * the default.
   *
   * @private
   * @param {string} type
   * @param {string} msg
   * @returns {string[]}
   * @memberof LoggerService
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
