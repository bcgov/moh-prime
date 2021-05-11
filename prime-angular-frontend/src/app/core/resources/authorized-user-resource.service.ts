import { Injectable } from '@angular/core';
import { ApiHttpResponse } from '@core/models/api-http-response.model';
import { LoggerService } from '@core/services/logger.service';
import { ToastService } from '@core/services/toast.service';
import { AuthorizedUser } from '@health-auth/shared/models/authorized-user.model';
import { HealthAuthorityEnum } from '@shared/enums/health-authority.enum';
import { Observable } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { ApiResource } from './api-resource.service';

@Injectable({
  providedIn: 'root'
})
export class AuthorizedUserResourceService {
  constructor(
    private apiResource: ApiResource,
    private toastService: ToastService,
    private logger: LoggerService
  ) { }

  public createAuthorizedUser(user: AuthorizedUser): Observable<AuthorizedUser> {
    return this.apiResource.post<AuthorizedUser>(`parties/authorized-users`, user)
      .pipe(
        map((response: ApiHttpResponse<AuthorizedUser>) => response.result),
        tap((newUser: AuthorizedUser) => {
          this.toastService.openSuccessToast('Authorized User has been created');
          this.logger.info('NEW_AUTHORIZED_USER', newUser);
        }),
        catchError((error: any) => {
          this.toastService.openErrorToast('Authorized User could not be created');
          this.logger.error('[SiteRegistration] AuthorizedUserResource::createAuthorizedUser error has occurred: ', error);
          throw error;
        })
      );
  }

  public updateAuthorizedUser(partyId: number, user: AuthorizedUser): Observable<AuthorizedUser> {
    return this.apiResource
      .put<AuthorizedUser>(`parties/authorized-users/${partyId}`, user)
      .pipe(
        map((response: ApiHttpResponse<AuthorizedUser>) => response.result),
        tap((updatedUser: AuthorizedUser) => {
          this.toastService.openSuccessToast('Authorized User has been updated');
          this.logger.info('UPDATED_AUTHORIZED_USER', updatedUser);
        }),
        catchError((error: any) => {
          this.toastService.openErrorToast('Authorized User could not be updated');
          this.logger.error('[SiteRegistration] AuthorizedUserResource::updateAuthorizedUser error has occurred: ', error);
          throw error;
        })
      );
  }

  public getAuthorizedUserById(partyId: number): Observable<AuthorizedUser> {
    return this.apiResource.get<AuthorizedUser>(`parties/authorized-users/${partyId}`)
      .pipe(
        map((response: ApiHttpResponse<AuthorizedUser>) => response.result),
        tap((user: AuthorizedUser) => this.logger.info('AUTHORIZED_USER', user)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Authorized User could not be retrieved');
          this.logger.error('[SiteRegistration] AuthorizedUserResource::getAuthorizedUserById error has occurred: ', error);
          throw error;
        })
      );
  }

  public getAuthorizedUserByUserId(userGuid: string): Observable<AuthorizedUser> {
    return this.apiResource.get<AuthorizedUser>(`parties/authorized-users/${userGuid}`)
      .pipe(
        map((response: ApiHttpResponse<AuthorizedUser>) => response.result),
        tap((user: AuthorizedUser) => this.logger.info('AUTHORIZED_USER', user)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Authorized User could not be retrieved');
          this.logger.error('[SiteRegistration] AuthorizedUserResource::getAuthorizedUserById error has occurred: ', error);
          throw error;
        })
      );
  }

  // TODO: Later this will be potentially moved into the organization.
  public getAuthorizedUsersByHealthAuthority(healthAuthorityCode: HealthAuthorityEnum): Observable<AuthorizedUser[]> {
    return this.apiResource.get<AuthorizedUser[]>(`health-authorities/${healthAuthorityCode}/authorized-users`)
      .pipe(
        map((response: ApiHttpResponse<AuthorizedUser[]>) => response.result),
        tap((users: AuthorizedUser[]) => this.logger.info('AUTHORIZED_USERS', users)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Authorized Users could not be retrieved');
          this.logger.error('[SiteRegistration] AuthorizedUserResource::getAuthorizedUsersByHA error has occurred: ', error);
          throw error;
        })
      );
  }
}
