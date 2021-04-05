import { Injectable } from '@angular/core';

import { NoContent, NoContentResponse } from '@core/resources/abstract-resource';

import { of } from 'rxjs';

import { ApiResource } from '@core/resources/api-resource.service';
import { ApiResourceUtilsService } from '@core/resources/api-resource-utils.service';
import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';

// TODO move to @lib/models
import { RemoteUser } from '@registration/shared/models/remote-user.model';

import { HealthAuthSite } from '@health-auth/shared/models/health-auth-site.model';

@Injectable({
  providedIn: 'root'
})
export class HealthAuthSiteRegResource {
  constructor(
    private apiResource: ApiResource,
    private apiResourceUtilsService: ApiResourceUtilsService,
    private toastService: ToastService,
    private logger: LoggerService
  ) { }

  public updateSite(site: HealthAuthSite): NoContent {
    return of().pipe(NoContentResponse);
  }

  public setSiteCompleted(siteId: number): NoContent {
    return of().pipe(NoContentResponse);
  }

  public removeSiteCompleted(siteId: number): NoContent {
    return of().pipe(NoContentResponse);
  }

  public sendRemoteUsersEmailAdmin(siteId: number): NoContent {
    return of().pipe(NoContentResponse);
  }

  public sendRemoteUsersEmailUser(siteId: number, newRemoteUsers: RemoteUser[]): NoContent {
    return of().pipe(NoContentResponse);
  }

  public submitSite(site: HealthAuthSite): NoContent {
    return of().pipe(NoContentResponse);
  }
}
