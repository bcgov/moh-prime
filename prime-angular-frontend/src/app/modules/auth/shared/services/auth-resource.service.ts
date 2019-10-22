import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';
import { map, catchError } from 'rxjs/operators';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { LoggerService } from '@core/services/logger.service';

import { AuthTokenService } from './auth-token.service';

@Injectable({
  providedIn: 'root'
})
export class AuthResource {

  constructor(
    @Inject(APP_CONFIG) private config: AppConfig,
    private http: HttpClient,
    private tokenService: AuthTokenService,
    private logger: LoggerService
  ) { }

  /**
   * Authenticate a user.
   *
   * @param {{ username: string, password: string }} payload
   * @returns {Observable<boolean>}
   * @memberof AuthResource
   */
  public login(payload: { username: string, password: string }): Observable<boolean> {
    // TODO: use CRUD service with API injector to cut down on boilerplate
    return this.http.post(`${this.config.apiEndpoint}/token`, payload)
      .pipe(
        map((response: { token: string }) => {
          this.tokenService.setToken(response.token);

          const claim = this.tokenService.decodeToken();

          this.logger.info('Authenticated!', claim);

          return true;
        })
      );
  }

  /**
   * Logout the authenticated user.
   *
   * @returns {Observable<any>}
   * @memberof AuthResource
   */
  // TODO: add observable type
  public logout(): Observable<any> {
    // TODO: use CRUD service with API injector to cut down on boilerplate
    return this.http.get(`${this.config.apiEndpoint}/logout`)
      .pipe(
        catchError((error: any) => {
          // TODO: determine response type for logout to remove token
          this.tokenService.removeToken();

          this.logger.info('Deauthenticated!');

          return error;
        })
      );
  }
}
