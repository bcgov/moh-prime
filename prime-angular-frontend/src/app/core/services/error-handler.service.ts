import { Injectable, Injector, ErrorHandler } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';

import { LoggerService } from '@core/services/logger.service';
import { DialogLogger } from '@shared/classes/dialog-logger';
import { environment } from '@env/environment.prod.template';

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
    const logger = this.injector.get(LoggerService);
    const dialogLogger = this.injector.get(DialogLogger);
    const router = this.injector.get(Router);

    if (error instanceof HttpErrorResponse) {
      // Server or connection error occurred
      if (!navigator.onLine) {
        // HTTP error intercept has occurred
        return logger.error('No Internet Connection');
      } else {
        // HTTP error has occurred (error.status = 403, 404, 500...)
        return logger.error(`${ error.status } - ${ error.message }`);
      }
    } else {
      // Client error has occurred (Angular Error, ReferenceError...)
      dialogLogger.log(error);
    }

    const message = (error.message)
      ? error.message
      : error.toString();
    const url = router.url;

    logger.error(message, { url });

    // IMPORTANT: Rethrow the error, otherwise it gets swallowed
    throw error;
  }
}
