import { Injectable } from '@angular/core';

import { NoContent, NoContentResponse } from '@core/resources/abstract-resource';

import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

import { Party } from '@lib/models/party.model';
import { ApiResource } from '@core/resources/api-resource.service';
import { ApiHttpResponse } from '@core/models/api-http-response.model';
import { ApiResourceUtilsService } from '@core/resources/api-resource-utils.service';
import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';

// TODO move to @lib/models
import { Organization } from '@registration/shared/models/organization.model';
import { RemoteUser } from '@registration/shared/models/remote-user.model';

import { AuthorizedUser } from '@health-auth/shared/models/authorized-user.model';
import { HealthAuthSite } from '@health-auth/shared/models/health-auth-site.model';
import { AuthorizedUserStatusEnum } from '@health-auth/shared/enums/authorized-user-status.enum';

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

  public getAuthorizedUserByUserId(userId: string): Observable<Party | null> {
    return this.apiResource.get<Party>(`parties/authorizeduser/${userId}`)
      .pipe(
        map((response: ApiHttpResponse<Party>) => response.result),
        tap((party: Party) => this.logger.info('SIGNING_AUTHORITY', party)),
        catchError((error: any) => {
          if (error.status === 404) {
            return of(null);
          }

          this.toastService.openErrorToast('Authorized user could not be retrieved');
          this.logger.error('[Core] HealthAuthSiteRegResource::getAuthorizedUserByUserId error has occurred: ', error);
          throw error;
        })
      );
  }

  public getAuthorizedUserById(partyId: number): Observable<Party | null> {
    return this.apiResource.get<Party>(`parties/authorizeduser/${partyId}`)
      .pipe(
        map((response: ApiHttpResponse<Party>) => response.result),
        tap((party: Party) => this.logger.info('SIGNING_AUTHORITY', party)),
        catchError((error: any) => {
          if (error.status === 404) {
            return of(null);
          }

          this.toastService.openErrorToast('Authorized user could not be retrieved');
          this.logger.error('[Core] HealthAuthSiteRegResource::getAuthorizedUser error has occurred: ', error);
          throw error;
        })
      );
  }

  public createAuthorizedUser(party: Party): Observable<Party> {
    return this.apiResource.post<Party>('parties/authorizeduser', party)
      .pipe(
        map((response: ApiHttpResponse<Party>) => response.result),
        tap((newParty: Party) => {
          this.toastService.openSuccessToast('Authorized user has been created');
          this.logger.info('NEW_SIGNING_AUTHORITY', newParty);
        }),
        catchError((error: any) => {
          this.toastService.openErrorToast('Authorized user could not be created');
          this.logger.error('[Core] HealthAuthSiteRegResource::createAuthorizedUser error has occurred: ', error);
          throw error;
        })
      );
  }

  public updateAuthorizedUser(party: Party): NoContent {
    return this.apiResource.put<NoContent>(`parties/authorizeduser/${party.id}`, party)
      .pipe(
        NoContentResponse,
        tap(() => this.toastService.openSuccessToast('Authorized user has been updated')),
        catchError((error: any) => {
          this.toastService.openErrorToast('Authorized user could not be updated');
          this.logger.error('[Core] HealthAuthSiteRegResource::updateAuthorizedUser error has occurred: ', error);
          throw error;
        })
      );
  }

  /**
   * @description
   * Get the organizations for a authorized user by user ID, and provide null when
   * a signing authority could not be found.
   */
  public getAuthorizedUserOrganizationsByUserId(userId: string): Observable<Organization[] | null> {
    return this.apiResource.get<Organization[]>(`parties/signingauthority/${userId}/organizations`)
      .pipe(
        map((response: ApiHttpResponse<Organization[]>) => response.result),
        tap((organizations: Organization[]) => this.logger.info('ORGANIZATIONS', organizations)),
        catchError((error: any) => {
          if (error.status === 404) {
            // No signing authority exists for the provided user ID
            return of(null);
          }

          this.toastService.openErrorToast('Organizations could not be retrieved');
          this.logger.error('[Core] OrganizationResource::getOrganizationsByUserId error has occurred: ', error);
          throw error;
        })
      );
  }


















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
