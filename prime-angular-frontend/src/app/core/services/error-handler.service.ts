import { Injectable, Injector, ErrorHandler } from '@angular/core';

import { LoggerService } from '@core/services/logger.service';
import { PathLocationStrategy, Location, LocationStrategy } from '@angular/common';

@Injectable({
  providedIn: 'root'
})
export class ErrorHandlerService implements ErrorHandler {

  constructor(
    // Can't use DI in constructor of error handler since it
    // is loaded first therefore have to use the injector
    private injector: Injector
  ) { }

  public handleError(error: any) {
    // TODO: check for instance of HttpErrorResponse
    const logger = this.injector.get(LoggerService);
    const location = this.injector.get(Location);
    const message = (error.message)
      ? error.message
      : error.toString();
    const url = (location instanceof PathLocationStrategy)
      ? location.path()
      : '';

    // TODO: implement stack trace and push to server for notification(s)
    logger.error(message, { url });

    // IMPORTANT: Rethrow the error, otherwise it gets swallowed
    throw error;
  }
}
