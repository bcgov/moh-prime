import { Injectable, Inject } from '@angular/core';
import { HttpHandler, HttpRequest, HttpEvent, HttpErrorResponse } from '@angular/common/http';
import { Router, NavigationExtras } from '@angular/router';

import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { LoggerService } from '@core/services/logger.service';
import { AuthService } from '@auth/shared/services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class ErrorHandlerInterceptor {
  constructor(
    @Inject(APP_CONFIG) private config: AppConfig,
    private router: Router,
    private logger: LoggerService
  ) { }

  /**
   * @description
   * Intercept 401 responses to redirect to login if a user
   * is not authenticated.
   */
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req)
      .pipe(
        catchError((error: any, caught) => {
          if (error instanceof HttpErrorResponse) {
            const status = error.status;
            if (status === 401) {
              this.logger.info('Unauthorized');
              // TODO handle unauthorized
            } else if (status === 422) {
              // TODO handle validation error messages
            } else if (status === 500) {
              // TODO handle internal server errors and messages
            } else if (status === 503) {
              this.router.navigate([this.config.routes.maintenance]);
            }

            return throwError(error);
          }
        })
      );
  }
}
