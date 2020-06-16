import { Injectable } from '@angular/core';
import { ApiResource } from '@core/resources/api-resource.service';
import { ApiResourceUtilsService } from '@core/resources/api-resource-utils.service';
import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { Observable } from 'rxjs';
import { Party } from '../models/party.model';
import { map, tap, catchError } from 'rxjs/operators';
import { ApiHttpResponse } from '@core/models/api-http-response.model';
import { NoContent } from '@core/resources/abstract-resource';
import { compare } from 'fast-json-patch';

@Injectable({
  providedIn: 'root'
})
export class PartyResource {

  constructor(
    private apiResource: ApiResource,
    private apiResourceUtilsService: ApiResourceUtilsService,
    private toastService: ToastService,
    private logger: LoggerService
  ) { }

  public createParty(party: Party): Observable<Party> {
    return this.apiResource.post<Party>(`parties`, party)
      .pipe(
        map((response: ApiHttpResponse<Party>) => response.result),
        tap((newParty: Party) => {
          this.toastService.openSuccessToast('Party has been created');
          this.logger.info('NEW_PARTY', newParty);
        }),
        catchError((error: any) => {
          this.toastService.openErrorToast('Party could not be created');
          this.logger.error('[SiteRegistration] PartyResource::createParty error has occurred: ', error);
          throw error;
        })
      );
  }

  public patchParty(initialParty: Party, updateParty: Party): NoContent {

    const jsonPatchDoc = compare(initialParty, updateParty);

    jsonPatchDoc.map((operation) => {
      // If physical address is being added, change replace to add
      if (initialParty?.physicalAddress?.city == null && updateParty?.physicalAddress?.city != null) {
        if (operation.path.includes('physicalAddress')) {
          operation.op = 'add';
        }
      }
      // If mailing address is being added, change replace to add
      if (initialParty?.mailingAddress?.city == null && updateParty?.mailingAddress?.city != null) {
        if (operation.path.includes('mailingAddress')) {
          operation.op = 'add';
        }
      }
    });

    return this.apiResource.patch<NoContent>(`parties/${updateParty.id}`, jsonPatchDoc)
      // TODO remove pipe when ApiResource handles NoContent
      .pipe(
        map(() => {
          this.toastService.openSuccessToast('Party has been patched');
        }),
        catchError((error: any) => {
          this.toastService.openErrorToast('Party could not be patched');
          this.logger.error('[SiteRegistration] SiteResource::patchParty error has occurred: ', error);
          throw error;
        })
      );
  }
}
