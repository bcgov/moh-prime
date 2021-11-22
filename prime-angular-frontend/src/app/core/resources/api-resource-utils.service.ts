import { Injectable } from '@angular/core';
import { HttpParams } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ApiResourceUtilsService {
  /**
   * @description
   * Make HttpParams from an object literal.
   */
  public makeHttpParams(
    queryParams: { [key: string]: any }
  ): HttpParams {
    return (queryParams)
      ? Object.keys(queryParams)
        .reduce(
          (httpParams: HttpParams, key: string) =>
            (![null, undefined].includes(queryParams[key]))
              ? this.createHttpParam(httpParams, key, queryParams[key])
              : httpParams,
          new HttpParams()
        )
      : null;
  }

  private createHttpParam(httpParams: HttpParams, key: string, value: any): HttpParams {
    return (Array.isArray(value))
      ? value.reduce((h, b) => this.createHttpParam(h, key, b), httpParams)
      : httpParams.append(key, `${value}`);
  }

  /**
   * @description
   * Helper for making the resource URI by inserting parameters after the
   * matching URI parameters, and provides only the remaining parameters
   * that were not inserted unless forced to maintain them in the
   * resourceParams for use as the payload or HttpParams.
   *
   * @example
   * const resourceUriParams = ['users', 'example', 'profiles', 'action']; // URI parameter(s)
   * const {
   *   resourceUri, // '/users/1/example/profiles/4/action'
   *   resourceParams // { other: true } for payload or HttpParams
   * } = this.makeResourceUri(resourceUriParams, { users: 1, profiles: 4, other: true });
   */
  // TODO replace URI params ie. /:id when param id found
  public makeResourceUri(
    resourceUriParams: string | string[] = '',
    params?: { [key: string]: any }
  ): { resourceUri: string, resourceParams: { [key: string]: any } } {
    if (resourceUriParams && Array.isArray(resourceUriParams)) {
      // Use the params to interpolate URI resource values, and
      // remove them from the list of parameters
      let resourceParams = params;
      // TODO update to use an array and join instead of string interpolation
      const resourceUri = resourceUriParams.reduce(
        (uri: string, resource: string) => {
          uri = (uri) ? `${uri}/${resource}` : `${resource}`;

          if (resourceParams && resourceParams[resource]) {
            const { [resource]: value, ...remaining } = resourceParams;
            resourceParams = remaining;
            uri = `${uri}/${value}`;
          }

          return uri;
        }, '');

      return { resourceUri, resourceParams };
    } else {
      // Otherwise, make no changes
      return { resourceUri: resourceUriParams as string, resourceParams: params };
    }
  }

  /**
   * @description
   * Helper for making the resource URI with HttpParams.
   */
  public makeResourceUriWithHttpParams(
    action: string = null,
    params: { [key: string]: any } = null,
    resourceUriParams: string | string[]
  ): { resourceUri: string, httpParams: HttpParams } {
    const uriParams = (action)
      ? (Array.isArray(resourceUriParams))
        ? [...resourceUriParams, action]
        : [resourceUriParams, action]
      : resourceUriParams;

    const { resourceUri, resourceParams } = this.makeResourceUri(uriParams, params);
    const httpParams = this.makeHttpParams(resourceParams);
    return { resourceUri, httpParams };
  }

  /**
   * @description
   * Helper for making a resource URI from an action.
   */
  public makeResourceUriFromAction(
    action: string,
    resourceUriParams: string | string[]
  ): string {
    return this.makeResourceUriWithHttpParams(action, null, resourceUriParams).resourceUri;
  }
}
