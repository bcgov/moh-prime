import { Injectable, Inject } from '@angular/core';
import { HttpHandler, HttpRequest, HttpEvent, HttpErrorResponse } from '@angular/common/http';
import { Router, NavigationExtras } from '@angular/router';

import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { AuthTokenService } from '@auth/shared/services/auth-token.service';
import { LoggerService } from '@core/services/logger.service';

@Injectable({
  providedIn: 'root'
})
// TODO: needs to be updated to reflect this application
export class ErrorHandlerInterceptor {
  constructor(
    @Inject(APP_CONFIG) private config: AppConfig,
    private router: Router,
    private tokenService: AuthTokenService,
    private logger: LoggerService
  ) { }

  /**
   * Intercept 401 responses to redirect to login if a user
   * is not authenticated.
   *
   * @param {HttpRequest<any>} req
   * @param {HttpHandler} next
   * @returns {Observable<HttpEvent<any>>}
   * @memberof ErrorInterceptor
   */
  // TODO: split into separate interceptors 401, 422, and 500
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req)
      .pipe(
        catchError((error: any, caught) => {
          // TODO: use exception service to handle
          if (error instanceof HttpErrorResponse) {
            const status = error.status;
            if (status === 401) {
              this.logger.info('Unauthorized');

              if (this.tokenService.hasToken) {
                this.tokenService.removeToken();
              }

              // TODO: store the logout action for global reuse with auth service
              const isLogout = req.url.includes('logout');
              let navigationExtras: NavigationExtras = {};

              if (!isLogout) {
                // Redirect to login for authentication, but create a
                // redirect URL to continue at current point once
                // re-authenticated for use when token expires
                const url: string = this.router.url;
                navigationExtras =
                  // Only if the redirect URL doesn't already exist or
                  // not transitioning to an auth route
                  (url.indexOf('redirectUrl') === -1 && url.indexOf('auth') === -1)
                    ? { queryParams: { redirectUrl: url } }
                    : { queryParamsHandling: 'preserve' };
              }

              this.router.navigate([this.config.routes.auth], navigationExtras);
            } else if (status === 422) {
              if (error.error.errors) {
                // TODO: use exception service?
                // Convert the validation error bag into default error
                // format, which only cares about a single error
                // TODO: configure to capture validation error(s)
                // const message = error.error.errors[Object.keys(error.error.errors)[0]][0];
                // error = { error: { message }, status };
              }
            } else if (status === 500) {
              // Provide a default 500 error message
              // TODO: use exception service?
              const message = 'An internal server error has occurred, and has been reported.';
              error = { error: { message }, status };
            }

            return throwError(error);
          }
        })
      );
  }
}
