import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';
import { map, catchError } from 'rxjs/operators';

import { environment } from '../../../environments/environment.prod';

import { APP_CONFIG, AppConfig } from 'src/app/app-config.module';

import { LoggerService } from '../services/logger.service';
import { AuthTokenService } from '../services/auth-token.service';

@Injectable({
  providedIn: 'root'
})
export class AuthResource {
  constructor(
    @Inject(APP_CONFIG) private config: AppConfig,
    private http: HttpClient,
    private logger: LoggerService,
    private tokenService: AuthTokenService
  ) { }

  public login(payload: { token: string }): Observable<boolean> {
    const resourceUri = `${environment.apiEndpoint}/token`;
    return this.http.post(resourceUri, payload)
      .pipe(
        map((response: { token: string }) => {
          this.tokenService.setToken(response.token);

          const claim = this.tokenService.decodeToken();

          this.logger.info('Authenticated!', claim);

          return true;
        })
      );
  }

}
