import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResource } from '../../core/resources/api-resource.service';
import { ApiResourceUtilsService } from '../../core/resources/api-resource-utils.service';
import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { map, tap, catchError } from 'rxjs/operators';
import { ApiHttpResponse } from '@core/models/api-http-response.model';
import { AddressAutocompleteFindResponse, AddressAutocompleteRetrieveResponse } from '@shared/models/address-autocomplete.model';

@Injectable({
  providedIn: 'root'
})
export class AddressValidationResource {

  constructor(
    private apiResource: ApiResource,
    private apiResourceUtilsService: ApiResourceUtilsService,
    private toastService: ToastService,
    private logger: LoggerService
  ) { }

  public find(searchTerm: string): Observable<AddressAutocompleteFindResponse[]> {
    return this.apiResource.get<AddressAutocompleteFindResponse[]>(`AddressValidation/find?searchTerm=${searchTerm}`)
      .pipe(
        map((response: ApiHttpResponse<any[]>) => response.result),
        // TODO split out into proper adapter

        tap((response: AddressAutocompleteFindResponse[]) => this.logger.info('AUTOCOMPLETE_FIND', response)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Autocomplete could not be retrieved');
          this.logger.error('[Shared] AddressValidationResource::find error has occurred: ', error);
          throw error;
        })
      );
  }

  public retrieve(id: string): Observable<AddressAutocompleteRetrieveResponse> {
    return this.apiResource.get<AddressAutocompleteRetrieveResponse>(`AddressValidation/retrieve?id=${id}`)
      .pipe(
        map((response: ApiHttpResponse<any>) => response.result),
        // TODO split out into proper adapter

        tap((response: AddressAutocompleteRetrieveResponse) => this.logger.info('AUTOCOMPLETE_RETRIEVE', response)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Autocomplete could not be retrieved');
          this.logger.error('[Shared] AddressValidationResource::find error has occurred: ', error);
          throw error;
        })
      );
  }
}
