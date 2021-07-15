import { Injectable, Inject } from '@angular/core';
import { HttpHandler, HttpRequest, HttpEvent, HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';

import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';

import { ConsoleLoggerService } from '@core/services/console-logger.service';

@Injectable({
  providedIn: 'root'
})
export class ErrorHandlerInterceptor {
  constructor(
    @Inject(APP_CONFIG) private config: AppConfig,
    private router: Router,
    private logger: ConsoleLoggerService
  ) { }

  /**
   * @description
   * Intercept error responses and provide handling.
   */
  public intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req)
      .pipe(
        catchError((error: any, caught) => {
          if (error instanceof HttpErrorResponse) {
            const status = error.status;
            switch (status) {
              case 400:
                this.logger.error('Bad Request', error);
                break;
              case 401:
                this.logger.error('Unauthorized', error);
                break;
              case 403:
                this.logger.error('Forbidden', error);
                break;
              case 404:
                this.logger.error('Not Found', error);
                break;
              case 422:
                this.logger.error('Unprocessable Entity', error);
                break;
              case 500:
                this.logger.error('Internal Server Error', error);
                break;
              case 503:
                this.logger.error('Service Unavailable', error);
                this.router.navigate([this.config.routes.maintenance]);
                break;
              default:
                this.logger.error('Unhandled HTTP response.', error);
                break;
            }

            return throwError(error);
          }
        })
      );
  }
}
