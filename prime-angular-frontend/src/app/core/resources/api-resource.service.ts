import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';

import { Observable } from 'rxjs';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';

import { AbstractResource } from '@core/resources/abstract-resource';
import { ApiHttpResponse } from '@core/models/api-http-response.model';
import { ApiHttpErrorResponse } from '@core/models/api-http-error-response.model';
import { ConsoleLoggerService } from '@core/services/console-logger.service';

@Injectable({
  providedIn: 'root'
})
export class ApiResource extends AbstractResource {
  constructor(
    protected logger: ConsoleLoggerService,
    @Inject(APP_CONFIG) private config: AppConfig,
    private http: HttpClient
  ) {
    super(logger);
  }

  public get<T>(
    path: string,
    params: HttpParams = new HttpParams(),
    options: { [key: string]: any } = {}
  ): Observable<ApiHttpResponse<T> | ApiHttpErrorResponse> {
    return this.http
      .get(`${this.config.apiEndpoint}/${path}`, { params, observe: 'response', ...options })
      .pipe(this.handleResponse<T>());
  }

  public post<T>(
    path: string,
    body: any = {},
    params: HttpParams = new HttpParams(),
    options: { [key: string]: any } = {}
  ): Observable<ApiHttpResponse<T> | ApiHttpErrorResponse> {
    return this.http
      .post(`${this.config.apiEndpoint}/${path}`, body, { params, observe: 'response', ...options })
      .pipe(this.handleResponse<T>());
  }

  public put<T>(
    path: string,
    body: any = {},
    params: HttpParams = new HttpParams(),
    options: { [key: string]: any } = {}
  ): Observable<ApiHttpResponse<T> | ApiHttpErrorResponse> {
    return this.http
      .put(`${this.config.apiEndpoint}/${path}`, body, { params, observe: 'response', ...options })
      .pipe(this.handleResponse<T>());
  }

  public patch<T>(
    path: string,
    body: any = {},
    params: HttpParams = new HttpParams(),
    options: { [key: string]: any } = {}
  ): Observable<ApiHttpResponse<T> | ApiHttpErrorResponse> {
    return this.http
      .patch(`${this.config.apiEndpoint}/${path}`, body, { params, observe: 'response', ...options })
      .pipe(this.handleResponse<T>());
  }

  public delete<T>(
    path: string,
    params: HttpParams = new HttpParams(),
    options: { [key: string]: any } = {}
  ): Observable<ApiHttpResponse<T> | ApiHttpErrorResponse> {
    return this.http
      .delete(`${this.config.apiEndpoint}/${path}`, { params, observe: 'response', ...options })
      .pipe(this.handleResponse<T>());
  }
}
