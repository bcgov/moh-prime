import { Observable, of } from 'rxjs';
import { environment } from '@env/environment';

import { AbstractLogger } from './abstract-logger';

/**
 * A logger logs to the console
 */
export class ConsoleLogger extends AbstractLogger {
  public log(type: string, params: { msg?: string, data?: any[] }): Observable<boolean> {
    let result = false;
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

      result = true;
    }
    return of(result);
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
