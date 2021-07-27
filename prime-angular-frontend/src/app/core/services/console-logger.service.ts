import { Injectable } from '@angular/core';

import { environment } from '@env/environment';
import { AbstractLoggerService } from '@core/services/abstract-logger.service';

@Injectable({
  providedIn: 'root'
})
export class ConsoleLoggerService extends AbstractLoggerService {

  constructor() {
    super();
  }

  /**
   * @description
   * Pretty print JSON.
   */
  public pretty(msg: string, ...data: any[]) {
    this.send('log', { msg, data: [JSON.stringify(data, null, '\t')] });
  }

  /**
   * @description
   * Prints the logging information, but ONLY if not in production.
   */
  protected send(type: string, params: { msg?: string, data?: any[] }) {
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
  protected colorize(type: string, msg: string): string[] {
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
