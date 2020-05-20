import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';
import { map, tap, catchError } from 'rxjs/operators';

import { ApiHttpResponse } from '@core/models/api-http-response.model';
import { ApiResource } from '@core/resources/api-resource.service';
import { ApiResourceUtilsService } from '@core/resources/api-resource-utils.service';
import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';

export interface OrgBookAutocompleteHttpResponse {
  first_index: number;
  last_index: number;
  results: OrgBookAutocompleteResult[];
  total: number;
}

export interface OrgBookAutocompleteResult {
  id: number;
  inactive: false;
  names: {
    credential_id: number;
    id: number;
    language: string;
    text: string;
    type: string;
  }[];
}

// TODO fill in with interfaces when needed
export interface OrgBookFacetHttpResponse {
  facets: any;
  objects: {
    // TODO much more in this object
    results: [
      {
        // TODO much more in this object
        topic: {
          // TODO much more in this object literal than source_id
          source_id: string; // Registration ID
        };
        related_topics: any[];
      }
    ]
  };
}

export interface OrgBookDetailHttpResponse {
  id: number;
  create_timestamp: string;
  source_id: string;
  type: string; // URL param: `registration`
  names: {
    id: number,
    credential_id: number,
    last_updated: string;
    inactive: boolean;
    text: string;
    language: string;
    issuer: any,
    type: string;
  }[];
  addresses: any[];
  attributes: {
    id: number;
    credential_id: number;
    credential_type_id: 1;
    last_updated: string;
    inactive: boolean;
    type: string;
    format: string;
    value: string;
  }[];
}

// TODO fill in with interface when needed
// export interface OrgBookRelatedHttpResponse { }

@Injectable({
  providedIn: 'root'
})
export class OrgBookResource {
  private readonly ORGBOOK_API_URL = 'https://www.orgbook.gov.bc.ca/api/v2';

  constructor(
    // TODO refactor ApiResource to be generic for creating OtherApiResources
    // private apiResource: ApiResource,
    private http: HttpClient,
    private apiResourceUtilsService: ApiResourceUtilsService,
    private toastService: ToastService,
    private logger: LoggerService
  ) { }

  public autocomplete(orgName: string, inactive: boolean = false): Observable<any> {
    const params = this.apiResourceUtilsService.makeHttpParams({ q: orgName, inactive });
    return this.http.get<OrgBookAutocompleteHttpResponse>(`${this.ORGBOOK_API_URL}/search/autocomplete`, { params })
      .pipe(
        map((response: OrgBookAutocompleteHttpResponse) => response),
        tap((response: OrgBookAutocompleteHttpResponse) => this.logger.info('ORGBOOK_AUTOCOMPLETE', response)),
        map((response: OrgBookAutocompleteHttpResponse) => response.results),
        catchError((error: any) => {
          // TODO should this even have a toast message?
          // this.toastService.openErrorToast('');
          this.logger.error('[Adjudication] OrgBookResource::autocomplete error has occurred: ', error);
          throw error;
        })
      );
  }

  /**
   * @description
   * Get organization facet, which provides the organization ID aka the  `source ID`, which
   * can be used to query more detailed information or relationships related to the
   * organization within the OrgBook API.
   *
   * @param orgName of the organization
   */
  public getOrganizationFacet(orgName: string) {
    const params = this.apiResourceUtilsService.makeHttpParams({ name: orgName });
    return this.http.get<OrgBookFacetHttpResponse>(`${this.ORGBOOK_API_URL}/search/credential/topic/facets`, { params })
      .pipe(
        map((response: OrgBookFacetHttpResponse) => response),
        tap((response: OrgBookFacetHttpResponse) => this.logger.info('ORGBOOK_FACET', response)),
        catchError((error: any) => {
          // TODO should this even have a toast message?
          // this.toastService.openErrorToast('');
          this.logger.error('[Adjudication] OrgBookResource::getOrganizationFacet error has occurred: ', error);
          throw error;
        })
      );
  }

  public getOrganizationDetail(sourceId: string) {
    return this.http.get<OrgBookDetailHttpResponse>(`${this.ORGBOOK_API_URL}/topic/ident/registration/${sourceId}/formatted`)
      .pipe(
        map((response: OrgBookDetailHttpResponse) => response),
        tap((response: OrgBookDetailHttpResponse) => this.logger.info('ORGBOOK_DETAIL', response)),
        catchError((error: any) => {
          // TODO should this even have a toast message?
          // this.toastService.openErrorToast('');
          this.logger.error('[Adjudication] OrgBookResource::getOrganizationBySourceId error has occurred: ', error);
          throw error;
        })
      );
  }

  // public getOrganizationRelatedTo(sourceId: string) {
  //   return this.http.get<OrgBookRelatedHttpResponse>(`${this.ORGBOOK_API_URL}/topic/${sourceId}/related_to`)
  //     .pipe(
  //       map((response: OrgBookRelatedHttpResponse) => response),
  //       tap((response: OrgBookRelatedHttpResponse) => this.logger.info('ORGBOOK_RELATED_TO', response)),
  //       catchError((error: any) => {
  //         // TODO should this even have a toast message?
  //         // this.toastService.openErrorToast('');
  //         this.logger.error('[Adjudication] OrgBookResource::getOrganizationRelated error has occurred: ', error);
  //         throw error;
  //       })
  //     );
  // }
}
