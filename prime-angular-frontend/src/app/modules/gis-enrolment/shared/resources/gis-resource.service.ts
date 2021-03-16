import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { ApiResourceUtilsService } from '@core/resources/api-resource-utils.service';
import { ApiResource } from '@core/resources/api-resource.service';
import { LoggerService } from '@core/services/logger.service';
import { ToastService } from '@core/services/toast.service';
import { NoContent, NoContentResponse } from '@core/resources/abstract-resource';

import { LdapCredential } from '../models/ldap-credential.model';

@Injectable({
  providedIn: 'root'
})
export class GisResource {
  constructor(
    private apiResource: ApiResource,
    private apiResourceUtilsService: ApiResourceUtilsService,
    private toastService: ToastService,
    private logger: LoggerService
  ) { }

  public ldapLogin(credentials: LdapCredential): Observable<NoContent> {
    return this.apiResource.post<NoContent>('gis/ldap/login', credentials)
      .pipe(
        NoContentResponse,
        catchError((error: any) => {
          this.toastService.openErrorToast('You could not be authenticated.');
          this.logger.error('[GisResource] ldapLogin error has occurred: ', error);
          throw error;
        })
      );
  }
}
