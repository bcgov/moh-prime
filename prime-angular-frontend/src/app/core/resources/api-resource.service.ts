import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpParams, HttpResponse, HttpStatusCode } from '@angular/common/http';

import { Observable, of, pipe, throwError, UnaryFunction } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';

import { AbstractResource } from '@core/resources/abstract-resource';
import { ApiHttpResponse } from '@core/models/api-http-response.model';
import { ApiHttpErrorResponse } from '@core/models/api-http-error-response.model';
import { ConsoleLoggerService } from '@core/services/console-logger.service';
import { ToastService } from '@core/services/toast.service';

@Injectable({
  providedIn: 'root'
})
export class ApiResource extends AbstractResource {
  constructor(
    protected logger: ConsoleLoggerService,
    protected toastService: ToastService,
    @Inject(APP_CONFIG) private config: AppConfig,
    private http: HttpClient
  ) {
    super(logger);
  }

  public get<T>(
    path: string,
    params: HttpParams = new HttpParams(),
    options: { [key: string]: any } = {},
    // TODO should be defaulted to true
    unwrapResult: boolean = false
  ): Observable<T | ApiHttpResponse<T> | ApiHttpErrorResponse> {
    return this.http
      .get(`${this.config.apiEndpoint}/${path}`, { params, observe: 'response', ...options })
      .pipe(
        this.handleResponsePipe<T>(),
        this.unwrapResultPipe<T>(unwrapResult)
      );
  }

  public head<T>(
    path: string,
    params: HttpParams = new HttpParams(),
    options: { [key: string]: any } = {}
  ): Observable<ApiHttpResponse<T> | ApiHttpErrorResponse> {
    return this.http
      .head(`${this.config.apiEndpoint}/${path}`, { params, observe: 'response', ...options })
      .pipe(this.handleResponsePipe<T>());
  }

  public post<T>(
    path: string,
    body: any = {},
    params: HttpParams = new HttpParams(),
    options: { [key: string]: any } = {},
    // TODO should be defaulted to true
    unwrapResult: boolean = false
  ): Observable<T | ApiHttpResponse<T> | ApiHttpErrorResponse> {
    return this.http
      .post(`${this.config.apiEndpoint}/${path}`, body, { params, observe: 'response', ...options })
      .pipe(
        this.handleResponsePipe<T>(),
        this.unwrapResultPipe<T>(unwrapResult)
      );
  }

  public put<T>(
    path: string,
    body: any = {},
    params: HttpParams = new HttpParams(),
    options: { [key: string]: any } = {},
    // TODO should be defaulted to true
    unwrapResult: boolean = false
  ): Observable<T | ApiHttpResponse<T> | ApiHttpErrorResponse> {
    return this.http
      .put(`${this.config.apiEndpoint}/${path}`, body, { params, observe: 'response', ...options })
      .pipe(
        this.handleResponsePipe<T>(),
        this.unwrapResultPipe<T>(unwrapResult)
      );
  }

  public patch<T>(
    path: string,
    body: any = {},
    params: HttpParams = new HttpParams(),
    options: { [key: string]: any } = {},
    // TODO should be defaulted to true
    unwrapResult: boolean = false
  ): Observable<T | ApiHttpResponse<T> | ApiHttpErrorResponse> {
    return this.http
      .patch(`${this.config.apiEndpoint}/${path}`, body, { params, observe: 'response', ...options })
      .pipe(
        this.handleResponsePipe<T>(),
        this.unwrapResultPipe<T>(unwrapResult)
      );
  }

  public delete<T>(
    path: string,
    params: HttpParams = new HttpParams(),
    options: { [key: string]: any } = {},
    // TODO should be defaulted to true
    unwrapResult: boolean = false
  ): Observable<T | ApiHttpResponse<T> | ApiHttpErrorResponse> {
    return this.http
      .delete(`${this.config.apiEndpoint}/${path}`, { params, observe: 'response', ...options })
      .pipe(
        this.handleResponsePipe<T>(),
        this.unwrapResultPipe<T>(unwrapResult)
      );
  }

  /**
   * @description
   * Handles getting the result from the response.
   */
  public unwrapResultPipe<T>(
    unwrapResults: boolean
  ): UnaryFunction<Observable<ApiHttpResponse<T>>, Observable<T | ApiHttpResponse<T>>> {
    return pipe(
      map((response: ApiHttpResponse<T>) =>
        (unwrapResults)
          ? response.result
          : response
      )
    );
  }

  /**
   * @description
   * Display the HTTP response in a toast for the user.
   *
   * @usage
   * pipe(tapToastPipe('Performed action was a success'))
   */
  public tapToastPipe(
    toastMessage: string
  ): UnaryFunction<Observable<unknown>, void> {
    return pipe(
      tap((_) => this.toastService.openSuccessToast(toastMessage))
    );
  }

  /**
   * @description
   * Display the HTTP response in the console.
   *
   * @usage
   * pipe(tapResultPipe<ModelName>('MODEL_NAME'))
   *
   * NOTE: Does not get displayed in production.
   */
  public tapResultPipe<T>(
    tapMessage: string
  ): UnaryFunction<Observable<T>, Observable<T>> {
    return pipe(
      tap((response: T) => this.logger.info(tapMessage, response))
    );
  }

  /**
   * @description
   * Handle an HTTP error response.
   */
  // TODO arity too high decide on reasonable error message format, and rely on stack trace
  public handleHttpResponseErrorPipe<T>(
    methodName: string,
    toastMessage: string | null = null,
    httpErrorCode?: HttpStatusCode,
    defaultResult?: T | ((error: any) => Observable<T>)
  ) {
    return pipe(
      this.handleHttpErrorMessagePipe(methodName, toastMessage),
      this.handleHttpErrorCodePipe<T>(httpErrorCode, defaultResult)
    );
  }

  /**
   * @description
   * Handles an HTTP error response by providing the
   * user and console with appropriate information.
   */
  // TODO don't use this instead combine into handleHttpErrorCodePipe so control
  //      of the toast is isolated, otherwise you would always see it
  public handleHttpErrorMessagePipe(
    methodName: string,
    toastMessage?: string
  ): UnaryFunction<unknown, Observable<never>> {
    return pipe(
      catchError((error: any) => {
        if (toastMessage) {
          this.toastService.openErrorToast(toastMessage);
        }
        this.logger.error(`[Error] Resource::${methodName} error has occurred: `, error);

        throw error;
      })
    );
  }

  /**
   * @description
   * Handles an expected HTTP error response.
   */
  public handleHttpErrorCodePipe<T = unknown>(
    httpErrorCode: HttpStatusCode,
    defaultResult: T | ((error: any) => Observable<T>)
  ): UnaryFunction<unknown, Observable<T | never>> {
    return pipe(
      catchError((error: any) => {
        if (error.status === httpErrorCode && defaultResult !== undefined) {
          return (defaultResult instanceof Function)
            ? defaultResult(error)
            : of(defaultResult);
        }

        throw error;
      })
    );
  };

  /**
   * @description
   * Handle the HTTP response.
   */
  protected handleResponsePipe<T>() {
    return pipe(
      map(this.handleSuccess<T>()),
      catchError(this.handleError)
    );
  }

  /**
   * @description
   * Handle a successful HTTP response.
   */
  protected handleSuccess<T>(): (response: HttpResponse<ApiHttpResponse<T>>) => ApiHttpResponse<T> {
    return ({ headers, status, body }: HttpResponse<ApiHttpResponse<T>>): ApiHttpResponse<T> => {
      this.logger.info(`RESPONSE: ${status}`, body);

      return { headers, status, ...body };
    };
  }

  /**
   * @description
   * Handle a erroneous HTTP response.
   */
  protected handleError({ status, error, headers, message }: HttpErrorResponse): Observable<ApiHttpErrorResponse> {
    if (error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      console.error('An error occurred:', error.error.message);
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      console.error(`Backend returned code ${status}, body was:`, error);
    }

    // TODO update to extend and take HttpErrorResponse as param to
    //      allow for static or instance methods
    return throwError(new ApiHttpErrorResponse(status, error, headers, message));
  }
}
