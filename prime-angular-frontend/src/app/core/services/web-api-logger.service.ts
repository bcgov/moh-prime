import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';

import { LogType } from '@core/models/log-type.enum';
import { LoggerResource } from '@core/resources/logger-resource.service';

import { AbstractLoggerService } from './abstract-logger.service';

@Injectable({
  providedIn: 'root'
})
export class WebApiLoggerService extends AbstractLoggerService {
  constructor(
    private loggerResource: LoggerResource,
  ) {
    super();
  }

  public error(msg: string, ...data: any[]): Observable<number> {
    return this.send('error', { msg, data });
  }

  protected send(type: string, params: { msg?: string; data?: any[]; }): Observable<number> {
    return this.loggerResource.createLog({ message: params.msg, data: JSON.stringify(params.data), logType: LogType[type.toUpperCase()] });
  }
}
