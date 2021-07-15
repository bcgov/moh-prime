import { Injectable } from '@angular/core';
import { environment } from '@env/environment.prod.template';

@Injectable({
  providedIn: 'root'
})
export abstract class AbstractLoggerService {
  constructor() { }

  /**
   * @description
   * General output of logging information.
   */
  public log(msg: string, ...data: any[]) {
    this.send('log', { msg, data });
  }

  /**
   * @description
   * Informative output of logging information.
   */
  public info(msg: string, ...data: any[]) {
    this.send('info', { msg, data });
  }

  /**
   * @description
   * Outputs a warning message.
   */
  public warn(msg: string, ...data: any[]) {
    this.send('warn', { msg, data });
  }

  /**
   * @description
   * Outputs an error message.
   */
  public error(msg: string, ...data: any[]) {
    this.send('error', { msg, data });
  }

  /**
   * @description
   * Outputs a stack trace.
   */
  public trace(msg: string, ...data: any[]) {
    this.send('error', { msg, data });
  }

  protected abstract send(type: string, params: { msg?: string, data?: any[] });

}
