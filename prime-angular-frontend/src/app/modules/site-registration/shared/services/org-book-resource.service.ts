import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable, pipe } from 'rxjs';
import { map, tap, catchError, switchMap } from 'rxjs/operators';

import { ApiHttpResponse } from '@core/models/api-http-response.model';
import { ApiResource } from '@core/resources/api-resource.service';
import { ApiResourceUtilsService } from '@core/resources/api-resource-utils.service';
import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { UtilsService, SortWeight } from '@core/services/utils.service';

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
          id: number;
          // TODO much more in this object literal than source_id
          source_id: string; // Registration ID
        };
        related_topics: any[];
      }
    ]
  };
}

export interface OrgBookDetailHttpResponse {
  id: number; // topic ID URL param for API requests
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
export interface OrgBookRelatedHttpResponse {
  topic_id: number;
  relation_id: number;
  credential: any;
  topic: {
    id: number;
    source_id: string;
    type: string;
    names: {
      id: number;
      credential_id: number;
      last_updated: string;
      inactive: boolean;
      text: string;
      language: any;
      issuer: any;
      type: string;
    }[];
  };
  related_topic: {
    id: number;
    type: string; // URL param: registration
    names: {
      id: number;
      text: string; // Name parameter
    }[];
    addresses: any[];
    attributes: {
      id: number;
      credential_id: number;
      credential_type_id: number;
      last_updated: string;
      inactive: boolean;
      type: string; // Possible type: relationship_description
      format: string;
      value: string;
    }[]
  };
  attributes: {
    id: number;
    type: string; // Possible type: relationship_description
    format: string;
    value: string; // Possible value: `Does Business As`
    credential_id: number;
  }[];
}

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
    private utilsService: UtilsService,
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
  public getOrganizationFacet(orgName: string): Observable<OrgBookFacetHttpResponse> {
    const params = this.apiResourceUtilsService.makeHttpParams({ name: orgName });
    return this.http.get<OrgBookFacetHttpResponse>(`${this.ORGBOOK_API_URL}/search/credential/topic/facets`, { params })
      .pipe(
        map((response: OrgBookFacetHttpResponse) => response),
        tap((response: OrgBookFacetHttpResponse) => this.logger.info('ORGBOOK_FACET', response)),
        catchError((error: any) => {
          // TODO should this even have a toast error message?
          // this.toastService.openErrorToast('');
          this.logger.error('[Adjudication] OrgBookResource::getOrganizationFacet error has occurred: ', error);
          throw error;
        })
      );
  }

  public getOrganizationDetail(sourceId: string): Observable<OrgBookDetailHttpResponse> {
    return this.http.get<OrgBookDetailHttpResponse>(`${this.ORGBOOK_API_URL}/topic/ident/registration/${sourceId}/formatted`)
      .pipe(
        map((response: OrgBookDetailHttpResponse) => response),
        tap((response: OrgBookDetailHttpResponse) => this.logger.info('ORGBOOK_DETAIL', response)),
        catchError((error: any) => {
          // TODO should this even have a toast error message?
          // this.toastService.openErrorToast('');
          this.logger.error('[Adjudication] OrgBookResource::getOrganizationBySourceId error has occurred: ', error);
          throw error;
        })
      );
  }

  public getOrganizationRelatedTo(topicId: number): Observable<OrgBookRelatedHttpResponse[]> {
    return this.http.get<OrgBookRelatedHttpResponse[]>(`${this.ORGBOOK_API_URL}/topic_relationship/${topicId}/related_to_relations`)
      .pipe(
        map((response: OrgBookRelatedHttpResponse[]) => response),
        tap((response: OrgBookRelatedHttpResponse[]) => this.logger.info('ORGBOOK_RELATED_TO', response)),
        catchError((error: any) => {
          // TODO should this even have a toast error message?
          // this.toastService.openErrorToast('');
          this.logger.error('[Adjudication] OrgBookResource::getOrganizationRelated error has occurred: ', error);
          throw error;
        })
      );
  }

  /**
   * @description
   * Extract the appropriate source ID.
   */
  public sourceIdMap() {
    return pipe(
      map((response: OrgBookFacetHttpResponse) => {
        // Assumed that only a single source ID will exist based on a
        // specific selection being made in autocomplete
        return response.objects.results[0].topic.source_id;
      })
    );
  }

  /**
   * @description
   * Get a list of "Doing Business As" based on a source ID.
   */
  public doingBusinessAsMap() {
    return pipe(
      switchMap((sourceId: string) => this.getOrganizationDetail(sourceId)),
      map((response: OrgBookDetailHttpResponse) => response.id),
      switchMap((topicId: number) => this.getOrganizationRelatedTo(topicId)),
      map((response: OrgBookRelatedHttpResponse[]) => {
        const doingBusinessAs = response
          .map((relation: OrgBookRelatedHttpResponse) => {
            // Assumed only a single name per organization is relavent
            const businessName = relation.related_topic.names[0].text;
            const isDoingBusinessAs = relation.attributes.some(a => a.value === 'Does Business As');
            return (isDoingBusinessAs) ? businessName : null;
          });
        // Remove duplicates since only names are persisted
        return [...new Set(doingBusinessAs)]
          .sort(this.sortDoingBusinessAsNames());
      }));
  }

  /**
   * @description
   * Sort by day of the week.
   */
  private sortDoingBusinessAsNames(): (a: string, b: string) => SortWeight {
    return (a: string, b: string) =>
      this.utilsService.sort<string>(a, b);
  }
}
