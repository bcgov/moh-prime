import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';
import { map, catchError } from 'rxjs/operators';

import { ApiResource } from '@core/resources/api-resource.service';
import { ApiResourceUtilsService } from '@core/resources/api-resource-utils.service';
import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { ApiHttpResponse } from '@core/models/api-http-response.model';

@Injectable({
  providedIn: 'root'
})
export class DocumentResource {
  constructor(
    private apiResource: ApiResource,
    private apiResourceUtilsService: ApiResourceUtilsService,
    private toastService: ToastService,
    private logger: LoggerService
  ) { }

  public getDocumentByGuid(guid: string): Observable<any> {
    return this.apiResource.get<string>(`document/${guid}`)
      .pipe(
        map((response: any) => response),
        catchError((error: any) => {
          this.toastService.openErrorToast('Document could not be retrieved');
          this.logger.error('[Shared] DocumentResource::getDocumentByGuid error has occurred: ', error);
          throw error;
        })
      );
  }
}
