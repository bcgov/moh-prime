import { Injectable } from '@angular/core';

import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

import { ApiHttpResponse } from '@core/models/api-http-response.model';
import { ConsoleLoggerService } from '@core/services/console-logger.service';
import { NoContent, NoContentResponse } from '@core/resources/abstract-resource';
import { ToastService } from '@core/services/toast.service';
import { ApiResource } from '@core/resources/api-resource.service';
import { AuthorizedUser } from '@shared/models/authorized-user.model';
import { HealthAuthoritySite } from '@health-auth/shared/models/health-authority-site.model';
import { HealthAuthoritySiteList } from '@health-auth/shared/models/health-authority-site-list.model';

@Injectable({
  providedIn: 'root'
})
export class AuthorizedUserResource {
  constructor(
    private apiResource: ApiResource,
    private logger: ConsoleLoggerService,
    private toastService: ToastService
  ) { }

  public getAuthorizedUserByUsername(username: string): Observable<AuthorizedUser | null> {
    return this.apiResource.get<AuthorizedUser>(`parties/authorized-users/${username}`)
      .pipe(
        map((response: ApiHttpResponse<AuthorizedUser>) => response.result),
        tap((authorizedUser: AuthorizedUser) => this.logger.info('AUTHORIZED_USER', authorizedUser)),
        catchError((error: any) => {
          if (error.status === 404) {
            return of(null);
          }

          this.toastService.openErrorToast('Authorized user could not be retrieved');
          this.logger.error('[Core] AuthorizedUserResource::getAuthorizedUserByUserId error has occurred: ', error);
          throw error;
        })
      );
  }

  public getAuthorizedUserById(authorizedUserId: number): Observable<AuthorizedUser | null> {
    return this.apiResource.get<AuthorizedUser>(`parties/authorized-users/${authorizedUserId}`)
      .pipe(
        map((response: ApiHttpResponse<AuthorizedUser>) => response.result),
        tap((authorizedUser: AuthorizedUser) => this.logger.info('AUTHORIZED_USER', authorizedUser)),
        catchError((error: any) => {
          if (error.status === 404) {
            return of(null);
          }

          this.toastService.openErrorToast('Authorized user could not be retrieved');
          this.logger.error('[Core] AuthorizedUserResource::getAuthorizedUser error has occurred: ', error);
          throw error;
        })
      );
  }

  public getAuthorizedUserSites(authorizedUserId: number): Observable<HealthAuthoritySiteList[]> {
    return this.apiResource.get<HealthAuthoritySiteList[]>(`parties/authorized-users/${authorizedUserId}/sites`, null, null, true)
      .pipe(
        map((healthAuthoritySites: HealthAuthoritySiteList[]) =>
          HealthAuthoritySiteList.toHealthAuthoritySiteList(healthAuthoritySites)
        ),
        tap((healthAuthoritySites: HealthAuthoritySiteList[]) => {
          this.logger.info('AUTHORIZED_USERS_SITES', healthAuthoritySites);
        }),
        catchError((error: any) => {
          this.toastService.openErrorToast('Authorized user could not be retrieved');
          this.logger.error('[Core] AuthorizedUserResource::getAuthorizedUserSites error has occurred: ', error);
          throw error;
        })
      );
  }

  public getAuthorizedUserSiteCount(authorizedUserId: number): Observable<number> {
    return this.apiResource.get<number>(`parties/authorized-users/${authorizedUserId}/site-count`)
      .pipe(
        map((response: ApiHttpResponse<number>) => response.result),
        tap((siteCount: number) => this.logger.info('siteCount', siteCount)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Authorized user site count could not be retrieved');
          this.logger.error('[Core] AuthorizedUserResource::getAuthorizedUserSiteCount error has occurred: ', error);
          throw error;
        })
      );
  }

  public createAuthorizedUser(authorizedUser: AuthorizedUser): Observable<AuthorizedUser> {
    return this.apiResource.post<AuthorizedUser>('parties/authorized-users', authorizedUser)
      .pipe(
        map((response: ApiHttpResponse<AuthorizedUser>) => response.result),
        tap((newAuthorizedUser: AuthorizedUser) => {
          this.toastService.openSuccessToast('Authorized user has been created');
          this.logger.info('NEW_AUTHORIZED_USER', newAuthorizedUser);
        }),
        catchError((error: any) => {
          this.toastService.openErrorToast('Authorized user could not be created');
          this.logger.error('[Core] AuthorizedUserResource::createAuthorizedUser error has occurred: ', error);
          throw error;
        })
      );
  }

  public updateAuthorizedUser(authorizedUser: AuthorizedUser): NoContent {
    return this.apiResource.put<NoContent>(`parties/authorized-users/${authorizedUser.id}`, authorizedUser)
      .pipe(
        NoContentResponse,
        tap(() => this.toastService.openSuccessToast('Authorized user has been updated')),
        catchError((error: any) => {
          this.toastService.openErrorToast('Authorized user could not be updated');
          this.logger.error('[Core] AuthorizedUserResource::updateAuthorizedUser error has occurred: ', error);
          throw error;
        })
      );
  }

  public activateAuthorizedUser(authorizedUserId: number): NoContent {
    return this.apiResource.post<NoContent>(`parties/authorized-users/${authorizedUserId}/activate`)
      .pipe(
        NoContentResponse,
        tap(() => this.toastService.openSuccessToast('Authorized user has been activated')),
        catchError((error: any) => {
          this.toastService.openErrorToast('Authorized user could not be activated');
          this.logger.error('[Core] AuthorizedUserResource::activateAuthorizedUser error has occurred: ', error);
          throw error;
        })
      );
  }

  public approveAuthorizedUser(authorizedUserId: number): NoContent {
    return this.apiResource.post<NoContent>(`parties/authorized-users/${authorizedUserId}/approve`)
      .pipe(
        NoContentResponse,
        tap(() => this.toastService.openSuccessToast('Authorized user has been approved')),
        catchError((error: any) => {
          this.toastService.openErrorToast('Authorized user could not be approved');
          this.logger.error('[Core] AuthorizedUserResource::approveAuthorizedUser error has occurred: ', error);
          throw error;
        })
      );
  }

  public deleteAuthorizedUser(authorizedUserId: number): NoContent {
    return this.apiResource.delete<NoContent>(`parties/authorized-users/${authorizedUserId}`)
      .pipe(
        NoContentResponse,
        tap(() => this.toastService.openSuccessToast('Authorized user has been deleted')),
        catchError((error: any) => {
          this.toastService.openErrorToast('Authorized user could not be deleted');
          this.logger.error('[Core] AuthorizedUserResource::deleteAuthorizedUser error has occurred: ', error);
          throw error;
        })
      );
  }

  public disableAuthorizedUser(authorizedUserId: number): NoContent {
    return this.apiResource.put<NoContent>(`parties/authorized-users/${authorizedUserId}/disable`)
      .pipe(
        NoContentResponse,
        tap(() => this.toastService.openSuccessToast('Authorized user has been disabled')),
        catchError((error: any) => {
          this.toastService.openErrorToast('Authorized user could not be disabled');
          this.logger.error('[Core] AuthorizedUserResource::disabledAuthorizedUser error has occurred: ', error);
          throw error;
        })
      );
  }

}
