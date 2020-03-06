import { HttpErrorResponse, HttpParams, HttpResponse } from '@angular/common/http';

import { Observable, throwError, pipe, of } from 'rxjs';
import { map, catchError } from 'rxjs/operators';

import { ApiHttpResponse } from '@core/models/api-http-response.model';
import { ApiHttpErrorResponse } from '@core/models/api-http-error-response.model';
import { LoggerService } from '@core/services/logger.service';

// Type for NoContent responses from the API
export type NoContent = Observable<void>;

export abstract class AbstractResource {
  constructor(
    protected logger: LoggerService
  ) { }

  public abstract get(
    path: string,
    params: HttpParams,
    options: { [key: string]: any }
  ): Observable<ApiHttpResponse<any> | ApiHttpErrorResponse>;

  public abstract post(
    path: string,
    body: { [key: string]: any },
    params: HttpParams,
    options: { [key: string]: any }
  ): Observable<ApiHttpResponse<any> | ApiHttpErrorResponse>;

  public abstract put(
    path: string,
    body: { [key: string]: any },
    params: HttpParams,
    options: { [key: string]: any }
  ): Observable<ApiHttpResponse<any> | ApiHttpErrorResponse>;

  public abstract delete(
    path: string,
    params: HttpParams,
    options: { [key: string]: any }
  ): Observable<ApiHttpResponse<any> | ApiHttpErrorResponse>;

  /**
   * @description
   * Handle the HTTP response.
   */
  protected handleResponse<T>() {
    return pipe(
      map(this.handleSuccess<T>()),
      catchError(this.handleError)
    );
  }

  /**
   * @description
   * Handle NoContent HTTP response.
   */
  // TODO handle NoContent
  protected handleNoContent<T>() {
    return (response: HttpResponse<ApiHttpResponse<T>>): Observable<HttpResponse<ApiHttpResponse<T>> | NoContent> =>
      (response.status === 204)
        ? of(void 0)
        : of(response);
  }

  /**
   * @description
   * Handle a successful HTTP response.
   */
  protected handleSuccess<T>(): (response: HttpResponse<ApiHttpResponse<T>>) => ApiHttpResponse<T> {
    return ({ status, body }: HttpResponse<ApiHttpResponse<T>>): ApiHttpResponse<T> => {
      this.logger.info(`RESPONSE: ${status}`, body);

      // TODO handle status of 204 NoContent using void 0
      return body;
    };
  }

  /**
   * @description
   * Handle a erroneous HTTP response.
   */
  protected handleError({ error, status }: HttpErrorResponse): Observable<ApiHttpErrorResponse> {
    if (error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      this.logger.error('An error occurred:', error.error.message);
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      this.logger.error(`Backend returned code ${error.status}, body was:`, error.error);
    }

    return throwError(new ApiHttpErrorResponse(error, status));
  }
}
