import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable, pipe } from 'rxjs';
import { map, tap, catchError, switchMap } from 'rxjs/operators';

import { ApiResourceUtilsService } from '@core/resources/api-resource-utils.service';
import { ConsoleLoggerService } from '@core/services/console-logger.service';
import { UtilsService, SortWeight } from '@core/services/utils.service';

import {
  OrgBookAutocompleteHttpResponse,
  OrgBookFacetHttpResponse,
  OrgBookDetailHttpResponse,
  OrgBookRelatedHttpResponse
} from '@registration/shared/models/orgbook.model';

@Injectable({
  providedIn: 'root'
})
export class OrgBookResource {
  private readonly ORGBOOK_API_URL = 'https://www.orgbook.gov.bc.ca/api/v2';

  constructor(
    private http: HttpClient,
    private apiResourceUtilsService: ApiResourceUtilsService,
    private utilsService: UtilsService,
    private logger: ConsoleLoggerService
  ) { }

  public autocomplete(orgName: string, inactive: boolean = false): Observable<OrgBookAutocompleteHttpResponse> {
    const params = this.apiResourceUtilsService.makeHttpParams({ q: orgName, inactive });
    return this.http.get<OrgBookAutocompleteHttpResponse>(`${this.ORGBOOK_API_URL}/search/autocomplete`, { params })
      .pipe(
        map((response: OrgBookAutocompleteHttpResponse) => response),
        tap((response: OrgBookAutocompleteHttpResponse) => this.logger.info('ORGBOOK_AUTOCOMPLETE', response)),
        catchError((error: any) => {
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
          this.logger.error('[Adjudication] OrgBookResource::getOrganizationFacet error has occurred: ', error);
          throw error;
        })
      );
  }

  public getOrganizationDetail(sourceId: string): Observable<OrgBookDetailHttpResponse> {
    return this.http.get<OrgBookDetailHttpResponse>(`${this.ORGBOOK_API_URL}/topic/ident/registration.registries.ca/${sourceId}/formatted`)
      .pipe(
        map((response: OrgBookDetailHttpResponse) => response),
        tap((response: OrgBookDetailHttpResponse) => this.logger.info('ORGBOOK_DETAIL', response)),
        catchError((error: any) => {
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
        const doingBusinessAs = response.reduce((businessNames: string[], relation: OrgBookRelatedHttpResponse) => {
          const isDoingBusinessAs = relation.attributes.some(a => a.value === 'Does Business As');
          if (isDoingBusinessAs) {
            // Assumed only a single name per organization is relevant
            const businessName = relation.related_topic.names[0].text;
            businessNames.push(businessName);
          }
          return businessNames;
        }, []);

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
