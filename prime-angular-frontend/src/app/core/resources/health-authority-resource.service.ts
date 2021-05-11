import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

import { ApiResource } from './api-resource.service';

import { ApiHttpResponse } from '@core/models/api-http-response.model';
import { LoggerService } from '@core/services/logger.service';
import { ToastService } from '@core/services/toast.service';

import { AuthorizedUser } from '@shared/models/authorized-user.model';
import { HealthAuthorityEnum } from '@shared/enums/health-authority.enum';

@Injectable({
  providedIn: 'root'
})
export class HealthAuthorityResource {

  constructor(
    private apiResource: ApiResource,
    private toastService: ToastService,
    private logger: LoggerService
  ) { }

  public getAuthorizedUserById(healthAuthorityCode: HealthAuthorityEnum, authorizedUserId: number): Observable<AuthorizedUser> {
    return this.apiResource.get<AuthorizedUser>(`health-authorities/${healthAuthorityCode}/authorized-users/${authorizedUserId}`)
      .pipe(
        map((response: ApiHttpResponse<AuthorizedUser>) => response.result),
        tap((user: AuthorizedUser) => this.logger.info('AUTHORIZED_USER', user)),
        catchError((error: any) => {
          if (error.status === 404) {
            return of(null);
          }

          this.toastService.openErrorToast('Authorized User could not be retrieved');
          this.logger.error('[SiteRegistration] HealthAuthorityResource::getAuthorizedUserById error has occurred: ', error);
          throw error;
        })
      );
  }

  public createAuthorizedUser(healthAuthorityCode: HealthAuthorityEnum, authorizedUser: AuthorizedUser): Observable<AuthorizedUser> {
    return this.apiResource.post<AuthorizedUser>(`health-authorities/${healthAuthorityCode}/authorized-users`, authorizedUser)
      .pipe(
        map((response: ApiHttpResponse<AuthorizedUser>) => response.result),
        tap((newUser: AuthorizedUser) => {
          this.toastService.openSuccessToast('Authorized User has been created');
          this.logger.info('NEW_AUTHORIZED_USER', newUser);
        }),
        catchError((error: any) => {
          this.toastService.openErrorToast('Site could not be created');
          this.logger.error('[SiteRegistration] HealthAuthorityResource::createAuthorizedUser error has occurred: ', error);
          throw error;
        })
      );
  }

  public updateAuthorizedUser(healthAuthorityCode: HealthAuthorityEnum, authorizedUser: AuthorizedUser): Observable<AuthorizedUser> {
    return this.apiResource
      .put<AuthorizedUser>(`health-authorities/${healthAuthorityCode}/authorized-users/${authorizedUser.id}`, authorizedUser)
      .pipe(
        map((response: ApiHttpResponse<AuthorizedUser>) => response.result),
        tap((updatedUser: AuthorizedUser) => {
          this.toastService.openSuccessToast('Authorized User has been updated');
          this.logger.info('UPDATED_AUTHORIZED_USER', updatedUser);
        }),
        catchError((error: any) => {
          this.toastService.openErrorToast('Site could not be updated');
          this.logger.error('[SiteRegistration] HealthAuthorityResource::updateAuthorizedUser error has occurred: ', error);
          throw error;
        })
      );
  }

  public deleteAuthorizedUser(healthAuthorityCode: HealthAuthorityEnum, authorizedUserId: number): Observable<AuthorizedUser> {
    return this.apiResource
      .delete<AuthorizedUser>(`health-authorities/${healthAuthorityCode}/authorized-users/${authorizedUserId}`)
      .pipe(
        map((response: ApiHttpResponse<AuthorizedUser>) => response.result),
        tap(() => this.toastService.openSuccessToast('Authorized User has been deleted')),
        catchError((error: any) => {
          this.toastService.openErrorToast('Authorized User could not be deleted');
          this.logger.error('[SiteRegistration] HealthAuthorityResource::deleteAuthorizedUser error has occurred: ', error);
          throw error;
        })
      );
  }

  public getAuthorizedUsersByHealthAuthority(healthAuthorityCode: HealthAuthorityEnum): Observable<AuthorizedUser[]> {
    return this.apiResource.get<AuthorizedUser[]>(`health-authorities/${healthAuthorityCode}/authorized-users`)
      .pipe(
        map((response: ApiHttpResponse<AuthorizedUser[]>) => response.result),
        tap((users: AuthorizedUser[]) => this.logger.info('AUTHORIZED_USERS', users)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Authorized Users could not be retrieved');
          this.logger.error('[SiteRegistration] HealthAuthorityResource::getAuthorizedUsersByHA error has occurred: ', error);
          throw error;
        })
      );
  }
}
