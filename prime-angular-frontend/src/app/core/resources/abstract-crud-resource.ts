import { Inject } from '@angular/core';
import { HttpParams } from '@angular/common/http';

import { Observable } from 'rxjs';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';

import { ApiHttpResponse } from '@core/models/api-http-response.model';
import { ApiHttpErrorResponse } from '@core/models/api-http-error-response.model';
import { CrudHttpResponse } from '@core/models/crud-http-response.model';
import { ApiResource } from '@core/resources/api-resource.service';
import { ApiResourceUtilsService } from '@core/resources/api-resource-utils.service';
import { ConsoleLoggerService } from '@core/services/console-logger.service';

export abstract class AbstractCrudResource<T> {
  /**
   * @description
   * Resource URI params.
   */
  protected resourceUriParams: string | string[];

  constructor(
    @Inject(APP_CONFIG) protected config: AppConfig,
    protected apiResource: ApiResource,
    protected apiResourceUtilsService: ApiResourceUtilsService,
    protected logger: ConsoleLoggerService
  ) { }

  /**
   * @description
   * Get a list of resources.
   */
  public index(
    params?: { [key: string]: any },
    resourceUriParams: string | string[] = this.resourceUriParams
  ): Observable<CrudHttpResponse<T[]> | ApiHttpErrorResponse> {
    const { resourceUri, httpParams } = this.makeResourceUriWithHttpParams(null, params, resourceUriParams);
    return this.apiResource.get<T[]>(resourceUri, httpParams);
  }

  /**
   * @description
   * Create a resource.
   */
  public create(
    resource: T,
    params: { [key: string]: any } = null,
    resourceUriParams: string | string[] = this.resourceUriParams
  ): Observable<ApiHttpResponse<T> | ApiHttpErrorResponse> {
    const { resourceUri, httpParams } = this.makeResourceUriWithHttpParams(null, params, resourceUriParams);
    return this.apiResource.post<T>(`${resourceUri}`, resource, httpParams);
  }

  /**
   * @description
   * Get a resource.
   */
  public get(
    id: number,
    params: { [key: string]: any } = null,
    resourceUriParams: string | string[] = this.resourceUriParams
  ): Observable<ApiHttpResponse<T> | ApiHttpErrorResponse> {
    const { resourceUri, httpParams } = this.makeResourceUriWithHttpParams(null, params, resourceUriParams);
    return this.apiResource.get<T>(`${resourceUri}/${id}`, httpParams);
  }

  /**
   * @description
   * Update a resource.
   */
  public update(
    id: number,
    resource: Partial<T>,
    params: { [key: string]: any } = null,
    resourceUriParams: string | string[] = this.resourceUriParams
  ): Observable<ApiHttpResponse<T> | ApiHttpErrorResponse> {
    const { resourceUri, httpParams } = this.makeResourceUriWithHttpParams(null, params, resourceUriParams);
    return this.apiResource.put<T>(`${resourceUri}/${id}`, resource, httpParams);
  }

  /**
   * @description
   * Delete a resource.
   */
  public delete(
    id: number,
    params: { [key: string]: any } = null,
    resourceUriParams: string | string[] = this.resourceUriParams
  ): Observable<ApiHttpResponse<T> | ApiHttpErrorResponse> {
    const { resourceUri, httpParams } = this.makeResourceUriWithHttpParams(null, params, resourceUriParams);
    return this.apiResource.delete<T>(`${resourceUri}/${id}`, httpParams);
  }

  /**
   * @description
   * Helper for making the resource URI with HttpParams for CRUD resources.
   */
  public makeResourceUriWithHttpParams(
    action: string = null,
    params: { [key: string]: any } = null,
    resourceUriParams: string | string[] = this.resourceUriParams
  ): { resourceUri: string, httpParams: HttpParams } {
    return this.apiResourceUtilsService.makeResourceUriWithHttpParams(action, params, resourceUriParams);
  }

  /**
   * @description
   * Helper for making a resource URI from an action for CRUD resources.
   */
  public makeResourceUriFromAction(
    action: string,
    resourceUriParams: string | string[] = this.resourceUriParams
  ): string {
    return this.apiResourceUtilsService.makeResourceUriFromAction(action, resourceUriParams);
  }
}
