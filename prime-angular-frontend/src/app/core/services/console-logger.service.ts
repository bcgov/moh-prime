import { Injectable } from '@angular/core';

import { LogType } from '@core/models/log-type.enum';
import { LoggerResource } from '@core/resources/logger-resource.service';
import { AbstractLoggerService } from '@core/services/abstract-logger.service';

@Injectable({
  providedIn: 'root'
})
export class ConsoleLoggerService extends AbstractLoggerService {

  constructor(
    private loggerResource: LoggerResource,
  ) {
    super();
  }

  /**
   * @description
   * Outputs an error message.
   */
  public error(msg: string, ...data: any[]) {
    this.print('error', { msg, data });
    this.loggerResource.createErrorLog({ message: msg, data: JSON.stringify(data), logType: LogType.ERROR }).subscribe();
  }
}
