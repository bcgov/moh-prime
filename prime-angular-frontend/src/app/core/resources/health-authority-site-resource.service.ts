import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

import { ToastService } from '@core/services/toast.service';
import { ApiResource } from '@core/resources/api-resource.service';
import { ConsoleLoggerService } from '@core/services/console-logger.service';
import { HealthAuthorityEnum } from '@lib/enums/health-authority.enum';

import { HealthAuthoritySite } from '@health-auth/shared/models/health-authority-site.model';
import { ApiHttpResponse } from '@core/models/api-http-response.model';
import { HealthAuthoritySiteCreate } from '@health-auth/shared/models/health-authority-site-create.model';

@Injectable({
  providedIn: 'root'
})
export class HealthAuthoritySiteResource {
  constructor(
    private apiResource: ApiResource,
    private toastService: ToastService,
    private logger: ConsoleLoggerService
  ) { }

  public createHealthAuthoritySite(healthAuthId: HealthAuthorityEnum, createModel: HealthAuthoritySiteCreate): Observable<HealthAuthoritySite> {
    return this.apiResource.post<HealthAuthoritySite>(`health-authorities/${healthAuthId}/sites`, createModel)
      .pipe(
        map((response: ApiHttpResponse<HealthAuthoritySite>) => response.result),
        tap((healthAuthoritySite: HealthAuthoritySite) => this.logger.info('HEALTH_AUTH_SITE', healthAuthoritySite)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Health authority site could not be created');
          this.logger.error('[Core] HealthAuthorityResource::createHealthAuthoritySite error has occurred: ', error);
          throw error;
        })
      );
  }

}
