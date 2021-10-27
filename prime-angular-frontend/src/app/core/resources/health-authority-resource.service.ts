import { Injectable } from '@angular/core';

import { NoContent, NoContentResponse } from '@core/resources/abstract-resource';

import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

import { ArrayUtils } from '@lib/utils/array-utils.class';
import { HealthAuthorityEnum } from '@lib/enums/health-authority.enum';
import { Contact } from '@lib/models/contact.model';
import { PrivacyOffice } from '@lib/models/privacy-office.model';
import { ApiResource } from '@core/resources/api-resource.service';
import { ApiHttpResponse } from '@core/models/api-http-response.model';
import { ApiResourceUtilsService } from '@core/resources/api-resource-utils.service';
import { ToastService } from '@core/services/toast.service';
import { ConsoleLoggerService } from '@core/services/console-logger.service';
import { CapitalizePipe } from '@shared/pipes/capitalize.pipe';
import { AuthorizedUser } from '@shared/models/authorized-user.model';
import { HealthAuthority } from '@shared/models/health-authority.model';
import { HealthAuthorityRow } from '@shared/models/health-authority-row.model';

import { HealthAuthoritySite, HealthAuthoritySiteDto } from '@health-auth/shared/models/health-authority-site.model';
import { HealthAuthoritySiteCreate } from '@health-auth/shared/models/health-authority-site-create.model';
import { HealthAuthoritySiteUpdate } from '@health-auth/shared/models/health-authority-site-update.model';

// TODO split this into multiple resources to reduce responsibility and have the
//      resource name accurately describe the service, possibly:
//      - HealthAuthorityResource,
//      - HealthAuthoritySiteResource, and maybe
//      - AuthorizedUserResource
@Injectable({
  providedIn: 'root'
})
export class HealthAuthorityResource {
  constructor(
    private apiResource: ApiResource,
    private apiResourceUtilsService: ApiResourceUtilsService,
    private toastService: ToastService,
    private logger: ConsoleLoggerService,
    private capitalizePipe: CapitalizePipe
  ) { }

  public getHealthAuthorities(): Observable<HealthAuthorityRow[]> {
    return this.apiResource.get<HealthAuthorityRow[]>(`health-authorities`)
      .pipe(
        map((response: ApiHttpResponse<HealthAuthorityRow[]>) => response.result),
        tap((healthAuthorities: HealthAuthorityRow[]) => this.logger.info('HEALTH_AUTHORITIES', healthAuthorities)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Health authorities could not be retrieved');
          this.logger.error('[Core] HealthAuthorityResource::getHealthAuthorities error has occurred: ', error);
          throw error;
        })
      );
  }

  public getHealthAuthorityById(healthAuthorityId: number): Observable<HealthAuthority> {
    return this.apiResource.get<HealthAuthority>(`health-authorities/${healthAuthorityId}`)
      .pipe(
        map((response: ApiHttpResponse<HealthAuthority>) => response.result),
        tap((healthAuthority: HealthAuthority) => this.logger.info('HEALTH_AUTHORITY', healthAuthority)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Health authority could not be retrieved');
          this.logger.error('[Core] HealthAuthorityResource::getHealthAuthorityById error has occurred: ', error);
          throw error;
        })
      );
  }

  public getAllHealthAuthoritySites(): Observable<HealthAuthoritySite[]> {
    return this.apiResource.get<HealthAuthoritySite[]>(`health-authorities/sites`)
      .pipe(
        map((response: ApiHttpResponse<HealthAuthoritySite[]>) => response.result),
        tap((healthAuthoritySites: HealthAuthoritySite[]) => this.logger.info('HEALTH_AUTHORITY_SITES', healthAuthoritySites)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Health authority sites could not be retrieved');
          this.logger.error('[Core] HealthAuthorityResource::getAllHealthAuthoritySites error has occurred: ', error);
          throw error;
        })
      );
  }

  public updateHealthAuthorityCareTypes(healthAuthorityId: number, careTypes: string[]): NoContent {
    return this.apiResource.put<NoContent>(`health-authorities/${healthAuthorityId}/care-types`, careTypes)
      .pipe(
        NoContentResponse,
        catchError((error: any) => {
          this.toastService.openErrorToast('Health authority care types could not be updated');
          this.logger.error('[Core] HealthAuthorityResource::updateCareTypes error has occurred: ', error);
          throw error;
        })
      );
  }

  public updateHealthAuthorityVendors(healthAuthorityId: number, vendorCodes: number[]): NoContent {
    return this.apiResource.put<NoContent>(`health-authorities/${healthAuthorityId}/vendors`, vendorCodes)
      .pipe(
        NoContentResponse,
        catchError((error: any) => {
          this.toastService.openErrorToast('Health authority vendors could not be updated');
          this.logger.error('[Core] HealthAuthorityResource::updateVendors error has occurred: ', error);
          throw error;
        })
      );
  }

  public updateHealthAuthorityPrivacyOffice(healthAuthorityId: number, privacyOffice: PrivacyOffice): NoContent {
    return this.apiResource.put<NoContent>(`health-authorities/${healthAuthorityId}/privacy-office`, privacyOffice)
      .pipe(
        NoContentResponse,
        catchError((error: any) => {
          this.toastService.openErrorToast('Health authority privacy office could not be updated');
          this.logger.error('[Core] HealthAuthorityResource::updatePrivacyOffice error has occurred: ', error);
          throw error;
        })
      );
  }

  public updateHealthAuthorityTechnicalSupports(healthAuthorityId: number, contacts: Contact[]): NoContent {
    return this.updateContacts(healthAuthorityId, 'technical-supports', contacts);
  }

  public updateHealthAuthorityPharmanetAdministrators(healthAuthorityId: number, contacts: Contact[]): NoContent {
    return this.updateContacts(healthAuthorityId, 'pharmanet-administrators', contacts);
  }

  public getAuthorizedUserByUserId(userId: string): Observable<AuthorizedUser | null> {
    return this.apiResource.get<AuthorizedUser>(`parties/authorized-users/${userId}`)
      .pipe(
        map((response: ApiHttpResponse<AuthorizedUser>) => response.result),
        tap((authorizedUser: AuthorizedUser) => this.logger.info('AUTHORIZED_USER', authorizedUser)),
        catchError((error: any) => {
          if (error.status === 404) {
            return of(null);
          }

          this.toastService.openErrorToast('Authorized user could not be retrieved');
          this.logger.error('[Core] HealthAuthorityResource::getAuthorizedUserByUserId error has occurred: ', error);
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
          this.logger.error('[Core] HealthAuthorityResource::getAuthorizedUser error has occurred: ', error);
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
          this.logger.error('[Core] HealthAuthorityResource::createAuthorizedUser error has occurred: ', error);
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
          this.logger.error('[Core] HealthAuthorityResource::updateAuthorizedUser error has occurred: ', error);
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
          this.logger.error('[Core] HealthAuthorityResource::activateAuthorizedUser error has occurred: ', error);
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
          this.logger.error('[Core] HealthAuthorityResource::approveAuthorizedUser error has occurred: ', error);
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
          this.logger.error('[Core] HealthAuthorityResource::deleteAuthorizedUser error has occurred: ', error);
          throw error;
        })
      );
  }

  public getAuthorizedUsersByHealthAuthority(healthAuthId: HealthAuthorityEnum): Observable<AuthorizedUser[]> {
    return this.apiResource.get<AuthorizedUser[]>(`health-authorities/${healthAuthId}/authorized-users`)
      .pipe(
        map((response: ApiHttpResponse<AuthorizedUser[]>) => response.result),
        tap((authorizedUsers: AuthorizedUser[]) => this.logger.info('AUTHORIZED_USERS', authorizedUsers)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Authorized users could not be retrieved');
          this.logger.error('[Core] HealthAuthorityResource::getAuthorizedUsersByHealthAuthority error has occurred: ', error);
          throw error;
        })
      );
  }

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

  public getHealthAuthoritySites(healthAuthId: HealthAuthorityEnum): Observable<HealthAuthoritySite[]> {
    return this.apiResource.get<HealthAuthoritySite[]>(`health-authorities/${healthAuthId}/sites`)
      .pipe(
        map((response: ApiHttpResponse<HealthAuthoritySite[]>) => response.result),
        tap((healthAuthoritySites: HealthAuthoritySite[]) => this.logger.info('HEALTH_AUTHORITY_SITES', healthAuthoritySites)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Health authority sites could not be retrieved');
          this.logger.error('[Core] HealthAuthorityResource::getHealthAuthoritySites error has occurred: ', error);
          throw error;
        })
      );
  }

  public getHealthAuthoritySiteById(healthAuthId: HealthAuthorityEnum, healthAuthSiteId: number): Observable<HealthAuthoritySite | null> {
    return this.apiResource.get<HealthAuthoritySiteDto>(`health-authorities/${healthAuthId}/sites/${healthAuthSiteId}`)
      .pipe(
        map((response: ApiHttpResponse<HealthAuthoritySiteDto>) => HealthAuthoritySite.toHealthAuthoritySite(response.result)),
        tap((healthAuthoritySite: HealthAuthoritySite) => this.logger.info('HEALTH_AUTHORITY_SITE', healthAuthoritySite)),
        catchError((error: any) => {
          if(error.status === 404) {
            return of(null);
          }

          this.toastService.openErrorToast('Health authority site could not be retrieved');
          this.logger.error('[Core] HealthAuthorityResource::getHealthAuthoritySiteById error has occurred: ', error);
          throw error;
        })
      );
  }

  public getHealthAuthoritySiteHoursOfOperation(healthAuthId: HealthAuthorityEnum, healthAuthSiteId: number): Observable<HealthAuthoritySite> {
    return this.apiResource.get<HealthAuthoritySite>(`health-authorities/${healthAuthId}/sites/${healthAuthSiteId}/hours-operation`)
      .pipe(
        map((response: ApiHttpResponse<HealthAuthoritySite>) => response.result),
        tap((healthAuthoritySite: HealthAuthoritySite) => this.logger.info('HEALTH_AUTHORITY_SITE_HOURS_OPERATION', healthAuthoritySite)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Health authority site could not be retrieved');
          this.logger.error('[Core] HealthAuthorityResource::getHealthAuthoritySiteHoursOfOperation error has occurred: ', error);
          throw error;
        })
      );
  }

  public getHealthAuthoritySiteRemoteUsers(healthAuthId: HealthAuthorityEnum, healthAuthSiteId: number): Observable<HealthAuthoritySite> {
    return this.apiResource.get<HealthAuthoritySite>(`health-authorities/${healthAuthId}/sites/${healthAuthSiteId}/remote-users`)
      .pipe(
        map((response: ApiHttpResponse<HealthAuthoritySite>) => response.result),
        tap((healthAuthoritySite: HealthAuthoritySite) => this.logger.info('HEALTH_AUTHORITY_SITE', healthAuthoritySite)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Health authority site could not be retrieved');
          this.logger.error('[Core] HealthAuthorityResource::getHealthAuthoritySiteRemoteUsers error has occurred: ', error);
          throw error;
        })
      );
  }

  public getHealthAuthoritySiteContacts(healthAuthId: HealthAuthorityEnum, healthAuthSiteId: number): Observable<{ label: string, email: string }[]> {
    return this.getHealthAuthoritySiteById(healthAuthId, healthAuthSiteId)
      .pipe(
        map((healthAuthSite: HealthAuthoritySite) => [
          // TODO what needs to happen and what is available
          // ...ArrayUtils.insertIf(healthAuthSite?.healthAuthorityPharmanetAdministrator, {
          //   label: 'PharmaNet Administrator',
          //   email: healthAuthSite?.healthAuthorityPharmanetAdministrator?.email
          // }),
          // ...ArrayUtils.insertIf(healthAuthSite?.healthAuthorityTechnicalSupport.email, {
          //   label: 'Technical Support Contact',
          //   email: healthAuthSite?.healthAuthorityTechnicalSupport.email
          // })
        ]),
        catchError((error: any) => {
          this.toastService.openErrorToast('Health authority site contacts could not be retrieved');
          this.logger.error('[Core] HealthAuthorityResource::getHealthAuthoritySiteContacts error has occurred: ', error);
          throw error;
        })
      );
  }

  public updateHealthAuthoritySite(healthAuthId: HealthAuthorityEnum, siteId: number, updateModel: HealthAuthoritySiteUpdate): NoContent {
    return this.apiResource.put<NoContent>(`health-authorities/${healthAuthId}/sites/${siteId}`, updateModel)
      .pipe(
        NoContentResponse,
        catchError((error: any) => {
          this.toastService.openErrorToast('Health authority site could not be updated');
          this.logger.error('[Core] HealthAuthorityResource::updateHealthAuthoritySite error has occurred: ', error);
          throw error;
        })
      );
  }

  /**
   * @description
   * Set the set as completed indicating the workflow has been entirely traversed
   * in wizard mode, and will now spoke between the views from overview.
   */
  public setHealthAuthoritySiteCompleted(healthAuthCode: number, siteId: number): NoContent {
    return this.apiResource.put<NoContent>(`health-authorities/${healthAuthCode}/sites/${siteId}/site-completed`)
      .pipe(
        NoContentResponse,
        catchError((error: any) => {
          this.toastService.openErrorToast('Health authority site could not be marked as completed');
          this.logger.error('[Core] HealthAuthorityResource::healthAuthoritySiteCompleted error has occurred: ', error);
          throw error;
        })
      );
  }

  /**
   * @description
   * Submit the health authority site registration.
   */
  public healthAuthoritySiteSubmit(healthAuthCode: number, siteId: number): NoContent {
    return this.apiResource.post<NoContent>(`health-authorities/${healthAuthCode}/sites/${siteId}/submit`)
      .pipe(
        NoContentResponse,
        catchError((error: any) => {
          this.toastService.openErrorToast('Health authority site could not be submitted');
          this.logger.error('[Core] HealthAuthorityResource::healthAuthoritySiteSubmit error has occurred: ', error);
          throw error;
        })
      );
  }

  private updateContacts(
    healthAuthorityId: number,
    contactType: 'privacy-officers' | 'technical-supports' | 'pharmanet-administrators',
    contact: Contact[]
  ): NoContent {
    return this.apiResource.put<NoContent>(`health-authorities/${healthAuthorityId}/${contactType}`, contact)
      .pipe(
        NoContentResponse,
        catchError((error: any) => {
          this.toastService.openErrorToast(`Health authority ${contactType.replace('-', ' ')} could not be updated`);
          this.logger.error(`[Core] HealthAuthorityResource::update${this.capitalizePipe.transform(contactType.replace('-', ' '), true)} error has occurred: `, error);
          throw error;
        })
      );
  }
}
