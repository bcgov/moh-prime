import { Injectable } from '@angular/core';

import { LogType } from '@core/models/log-type.enum';
import { LoggerResource } from '@core/resources/logger-resource.service';

import { AbstractLoggerService } from './abstract-logger.service';

@Injectable({
  providedIn: 'root'
})
export class HttpLoggerService extends AbstractLoggerService {
  constructor(
    private loggerResource: LoggerResource,
  ) {
    super();
  }

  protected send(type: string, params: { msg?: string; data?: any[]; }) {
    this.loggerResource.createLog({ message: params.msg, data: JSON.stringify(params.data), logType: LogType[type.toUpperCase()] }).subscribe();
  }
}
