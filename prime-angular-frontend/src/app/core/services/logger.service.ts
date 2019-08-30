import { Injectable } from '@angular/core';

import { environment } from '../../../environments/environment';

@Injectable()
export class LoggerService {
  constructor() { }

  public log(msg: string, ...data: any[]) {
    this.print('log', { msg, data });
  }

  public info(msg: string, ...data: any[]) {
    this.print('info', { msg, data });
  }

  public warn(msg: string, ...data: any[]) {
    this.print('warn', { msg, data });
  }

  public error(msg: string, ...data: any[]) {
    this.print('error', { msg, data });
  }

  public pretty(msg: string, ...data: any[]) {
    this.print('log', { msg, data: [JSON.stringify(data, null, '\t')] });
  }

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
