import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

import { ApiResource } from './api-resource.service';

import { ApiHttpResponse } from '@core/models/api-http-response.model';
import { LoggerService } from '@core/services/logger.service';
import { ToastService } from '@core/services/toast.service';

import { HAAuthorizedUser } from '@shared/models/ha-authorized-user.model';
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

  public createAuthorizedUser(healthAuthorityCode: HealthAuthorityEnum, user: HAAuthorizedUser): Observable<HAAuthorizedUser> {
    return this.apiResource.post<HAAuthorizedUser>(`health-authorities/${healthAuthorityCode}/authorized-users`, user)
      .pipe(
        map((response: ApiHttpResponse<HAAuthorizedUser>) => response.result),
        tap((newUser: HAAuthorizedUser) => {
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

  public updateAuthorizedUser(healthAuthorityCode: HealthAuthorityEnum, authorizedUserId: number, user: HAAuthorizedUser): Observable<HAAuthorizedUser> {
    return this.apiResource
      .put<HAAuthorizedUser>(`health-authorities/${healthAuthorityCode}/authorized-users/${authorizedUserId}`, user)
      .pipe(
        map((response: ApiHttpResponse<HAAuthorizedUser>) => response.result),
        tap((updatedUser: HAAuthorizedUser) => {
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

  public deleteAuthorizedUser(healthAuthorityCode: HealthAuthorityEnum, authorizedUserId: number): Observable<HAAuthorizedUser> {
    return this.apiResource
      .delete<HAAuthorizedUser>(`health-authorities/${healthAuthorityCode}/authorized-users/${authorizedUserId}`)
      .pipe(
        map((response: ApiHttpResponse<HAAuthorizedUser>) => response.result),
        tap(() => {
          this.toastService.openSuccessToast('Authorized User has been deleted');
        }),
        catchError((error: any) => {
          this.toastService.openErrorToast('Authorized User could not be deleted');
          this.logger.error('[SiteRegistration] HealthAuthorityResource::deleteAuthorizedUser error has occurred: ', error);
          throw error;
        })
      );
  }

  public getAuthorizedUserById(healthAuthorityCode: HealthAuthorityEnum, authorizedUserId: number): Observable<HAAuthorizedUser> {
    return this.apiResource.get<HAAuthorizedUser>(`health-authorities/${healthAuthorityCode}/authorized-users/${authorizedUserId}`)
      .pipe(
        map((response: ApiHttpResponse<HAAuthorizedUser>) => response.result),
        tap((user: HAAuthorizedUser) => this.logger.info('AUTHORIZED_USER', user)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Authorized User could not be retrieved');
          this.logger.error('[SiteRegistration] HealthAuthorityResource::getAuthorizedUserById error has occurred: ', error);
          throw error;
        })
      );
  }

  public getAuthorizedUsersByHA(healthAuthorityCode: HealthAuthorityEnum): Observable<HAAuthorizedUser[]> {
    return this.apiResource.get<HAAuthorizedUser[]>(`health-authorities/${healthAuthorityCode}/authorized-users`)
      .pipe(
        map((response: ApiHttpResponse<HAAuthorizedUser[]>) => response.result),
        tap((users: HAAuthorizedUser[]) => this.logger.info('AUTHORIZED_USERS', users)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Authorized Users could not be retrieved');
          this.logger.error('[SiteRegistration] HealthAuthorityResource::getAuthorizedUsersByHA error has occurred: ', error);
          throw error;
        })
      );
  }
}
