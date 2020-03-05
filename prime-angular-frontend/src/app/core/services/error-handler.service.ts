import { Injectable, Injector, ErrorHandler } from '@angular/core';
import { PathLocationStrategy, LocationStrategy } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';

import { LoggerService } from '@core/services/logger.service';
import { ToastService } from '@core/services/toast.service';

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
    const location = this.injector.get<LocationStrategy>(PathLocationStrategy);
    const router = this.injector.get(Router);
    const toastService = this.injector.get(ToastService);

    if (error instanceof HttpErrorResponse) {
      // Server or connection error occurred
      if (!navigator.onLine) {
        // HTTP error has occurred
        return toastService.openErrorToast('No Internet Connection');
      } else {
        // HTTP error has occurred (error.status = 403, 404, 500...)
        return toastService.openErrorToast(`${error.status} - ${error.message}`);
      }
    } else {
      // Client error has occurred (Angular Error, ReferenceError...)
      router.navigate([], { queryParams: { error } });
    }

    const message = (error.message)
      ? error.message
      : error.toString();
    const url = location.path();

    // TODO implement stack trace js and push to server for logging
    logger.error(message, { url });

    // TODO should this rethrow?
    // IMPORTANT: Rethrow the error, otherwise it gets swallowed
    throw error;
  }
}
