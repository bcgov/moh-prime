import { Injectable, Injector, ErrorHandler } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';

import { ApiHttpErrorResponse } from '@core/models/api-http-error-response.model';

import { DialogLogger } from '@shared/classes/dialog-logger';
import { ConsoleLoggerService } from './console-logger.service';
import { WebApiLoggerService } from './web-api-logger.service';

@Injectable({
  providedIn: 'root'
})
export class ErrorHandlerService implements ErrorHandler {
  constructor(
    // Can't use DI in constructor of error handler since it's
    // loaded first therefore have to use the injector
    private injector: Injector
  ) { }

  public handleError(error: Error | HttpErrorResponse) {
    const logger = this.injector.get(ConsoleLoggerService);
    const webApiLogger = this.injector.get(WebApiLoggerService);
    const dialogLogger = this.injector.get(DialogLogger);
    const router = this.injector.get(Router);

    const message = (error.message)
      ? error.message
      : error.toString();
    const url = router.url;

    if (error instanceof HttpErrorResponse || error instanceof ApiHttpErrorResponse) {
      // Server or connection error occurred
      if (!navigator.onLine) {
        // HTTP error intercept has occurred
        return logger.error('No Internet Connection');
      } else {
        // HTTP error has occurred (error.status = 403, 404, 500...)
        return logger.error(`${error.status} - ${error.message}`);
      }
    } else {
      // Client error has occurred (Angular Error, ReferenceError...)
      webApiLogger.error(message, { url })
        .subscribe(logId => dialogLogger.log(logId));
    }

    logger.error(message, { url });
  }
}
