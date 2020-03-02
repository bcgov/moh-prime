import { Injectable, Injector, ErrorHandler } from '@angular/core';
import { PathLocationStrategy, LocationStrategy } from '@angular/common';

import { LoggerService } from '@core/services/logger.service';

@Injectable({
  providedIn: 'root'
})
export class ErrorHandlerService implements ErrorHandler {

  constructor(
    // Can't use DI in constructor of error handler since it's
    // loaded first therefore have to use the injector
    private injector: Injector
  ) { }

  public handleError(error: any) {
    // TODO check for instance of HttpErrorResponse
    const logger = this.injector.get(LoggerService);
    const location = this.injector.get<LocationStrategy>(PathLocationStrategy);

    const message = (error.message)
      ? error.message
      : error.toString();
    const url = location.path();

    // TODO implement stack trace and push to server for notification(s)
    logger.error(message, { url });

    // IMPORTANT: Rethrow the error, otherwise it gets swallowed
    throw error;
  }
}
