import { HttpErrorResponse, HttpParams, HttpResponse } from '@angular/common/http';

import { Observable, throwError, pipe, of } from 'rxjs';
import { map, catchError } from 'rxjs/operators';

import { ApiHttpResponse } from '@core/models/api-http-response.model';
import { ApiHttpErrorResponse } from '@core/models/api-http-error-response.model';
import { ConsoleLoggerService } from '@core/services/console-logger.service';

// Type for NoContent responses from the API
export type NoContent = Observable<void>;
export const NoContentResponse = pipe(map(() => void 0));

export abstract class AbstractResource {
  protected constructor(
    protected logger: ConsoleLoggerService
  ) { }

  public abstract get(
    path: string,
    params: HttpParams,
    options: { [key: string]: any; }
  ): Observable<ApiHttpResponse<any> | ApiHttpErrorResponse>;

  public abstract head(
    path: string,
    params: HttpParams,
    options: { [key: string]: any; }
  ): Observable<ApiHttpResponse<any> | ApiHttpErrorResponse>;

  public abstract post(
    path: string,
    body: { [key: string]: any; },
    params: HttpParams,
    options: { [key: string]: any; }
  ): Observable<ApiHttpResponse<any> | ApiHttpErrorResponse>;

  public abstract put(
    path: string,
    body: { [key: string]: any; },
    params: HttpParams,
    options: { [key: string]: any; }
  ): Observable<ApiHttpResponse<any> | ApiHttpErrorResponse>;

  public abstract delete(
    path: string,
    params: HttpParams,
    options: { [key: string]: any; }
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
  // TODO handle NoContent and possibly drop NoContentResponse
  protected handleNoContent() {
    return (_: HttpResponse<ApiHttpResponse<NoContent>>): Observable<NoContent> => of(void 0);
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

    return throwError(new ApiHttpErrorResponse(status, error, headers, message));
  }
}
