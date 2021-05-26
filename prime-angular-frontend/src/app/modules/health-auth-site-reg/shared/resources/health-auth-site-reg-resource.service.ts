import { Injectable } from '@angular/core';

import { NoContent, NoContentResponse } from '@core/resources/abstract-resource';

import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

import { ApiResource } from '@core/resources/api-resource.service';
import { ApiHttpResponse } from '@core/models/api-http-response.model';
import { ApiResourceUtilsService } from '@core/resources/api-resource-utils.service';
import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
// TODO move to @lib/models
import { AuthorizedUser } from '@shared/models/authorized-user.model';

// TODO move to @lib/models
import { Organization } from '@registration/shared/models/organization.model';
import { RemoteUser } from '@registration/shared/models/remote-user.model';

import { HealthAuthSite } from '@health-auth/shared/models/health-auth-site.model';

/**
 * @description
 * Health authority site registration specific resource used throughout
 * an enrolment.
 *
 * NOTE: For shared endpoints with other modules see HealthAuthorityResource.
 */
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
