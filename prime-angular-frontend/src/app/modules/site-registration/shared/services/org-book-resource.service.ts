import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';
import { map, tap, catchError } from 'rxjs/operators';

import { ApiHttpResponse } from '@core/models/api-http-response.model';
import { ApiResource } from '@core/resources/api-resource.service';
import { ApiResourceUtilsService } from '@core/resources/api-resource-utils.service';
import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';

@Injectable({
  providedIn: 'root'
})
export class OrgBookResource {
  private readonly ORGBOOK_API_URL = 'https://www.orgbook.gov.bc.ca/api/v2';

  constructor(
    private apiResource: ApiResource,
    private apiResourceUtilsService: ApiResourceUtilsService,
    private toastService: ToastService,
    private logger: LoggerService
  ) { }

  public searchOrgBook(search: string): Observable<any> {
    const params = this.apiResourceUtilsService.makeHttpParams({ category: search });
    return this.apiResource.get<any>(`${this.ORGBOOK_API_URL}/search/autocomplete`, params)
      .pipe(
        map((response: ApiHttpResponse<any>) => response),
        tap((response: any) => this.logger.info('RESPONSE', response)),
        map((response: any) => response),
        catchError((error: any) => {
          this.toastService.openErrorToast('');
          this.logger.error('[Adjudication] OrgBookResource::getSearchOrgBook error has occurred: ', error);
          throw error;
        })
      );
  }

  public getOrganization(name: string) {
    const params = this.apiResourceUtilsService.makeHttpParams({ name });
    return this.apiResource.get<any>(`${this.ORGBOOK_API_URL}/search/credential/topic/facets`, params)
      .pipe(
        map((response: ApiHttpResponse<any>) => response),
        tap((response: any) => this.logger.info('RESPONSE', response)),
        map((response: any) => response),
        catchError((error: any) => {
          this.toastService.openErrorToast('');
          this.logger.error('[Adjudication] OrgBookResource::getSearchOrgBook error has occurred: ', error);
          throw error;
        })
      );
  }

  // http://test.orgbook.gov.bc.ca/api/v2/topic/ident/registration/S0030754/formatted
}
