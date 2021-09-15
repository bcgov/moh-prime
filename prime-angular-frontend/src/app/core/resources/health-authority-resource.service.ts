import { Injectable } from '@angular/core';

import { NoContent, NoContentResponse } from '@core/resources/abstract-resource';

import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

import { ArrayUtils } from '@lib/utils/array-utils.class';
import { Contact } from '@lib/models/contact.model';
import { ApiResource } from '@core/resources/api-resource.service';
import { ApiHttpResponse } from '@core/models/api-http-response.model';
import { ApiResourceUtilsService } from '@core/resources/api-resource-utils.service';
import { ToastService } from '@core/services/toast.service';
import { ConsoleLoggerService } from '@core/services/console-logger.service';
import { CapitalizePipe } from '@shared/pipes/capitalize.pipe';
import { AuthorizedUser } from '@shared/models/authorized-user.model';
import { HealthAuthority } from '@shared/models/health-authority.model';
import { HealthAuthorityRow } from '@shared/models/health-authority-row.model';
import { HealthAuthorityEnum } from '@shared/enums/health-authority.enum';
// TODO move models into lib
import { PrivacyOffice } from '@adjudication/shared/models/privacy-office.model';
import { HealthAuthoritySite } from '@health-auth/shared/models/health-authority-site.model';
import { VendorForm } from '@health-auth/pages/vendor-page/vendor-form.model';
import { SiteInformationForm } from '@health-auth/pages/site-information-page/site-information-form.model';
import { HealthAuthCareTypeForm } from '@health-auth/pages/health-auth-care-type-page/health-auth-care-type-form.model';
import { SiteAddressForm } from '@health-auth/pages/site-address-page/site-address-form.model';
import { HoursOperationForm } from '@health-auth/pages/hours-operation-page/hours-operation-form.model';
import { RemoteUsersForm } from '@health-auth/pages/remote-users-page/remote-users-form.model';
import { AdministratorForm } from '@health-auth/pages/administrator-page/administrator-form.model';

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

  public getAuthorizedUsersByHealthAuthority(healthAuthorityCode: HealthAuthorityEnum): Observable<AuthorizedUser[]> {
    return this.apiResource.get<AuthorizedUser[]>(`health-authorities/${healthAuthorityCode}/authorized-users`)
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

  public createHealthAuthoritySite(healthAuthId: number, payload: VendorForm): Observable<HealthAuthoritySite> {
    return this.apiResource.post<HealthAuthoritySite>(`health-authorities/${healthAuthId}/sites`, payload)
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

  public getHealthAuthoritySites(healthAuthId: number): Observable<HealthAuthoritySite[]> {
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

  public getHealthAuthoritySiteById(healthAuthId: number, healthAuthSiteId: number): Observable<HealthAuthoritySite> {
    return this.apiResource.get<HealthAuthoritySite>(`health-authorities/${healthAuthId}/sites/${healthAuthSiteId}`)
      .pipe(
        map((response: ApiHttpResponse<HealthAuthoritySite>) => response.result),
        tap((healthAuthoritySite: HealthAuthoritySite) => this.logger.info('HEALTH_AUTHORITY_SITE', healthAuthoritySite)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Health authority site could not be retrieved');
          this.logger.error('[Core] HealthAuthorityResource::getHealthAuthoritySiteById error has occurred: ', error);
          throw error;
        })
      );
  }

  public getHealthAuthoritySiteContacts(healthAuthId: number, healthAuthSiteId: number): Observable<{ label: string, email: string }[]> {
    // TODO create separate endpoint to get health auth contacts
    return this.getHealthAuthoritySiteById(healthAuthId, healthAuthSiteId)
      .pipe(
        map((healthAuthSite: HealthAuthoritySite) => [
          // TODO no authorized user on health auth site view model
          // {
          //   label: 'Authorized User',
          //   email: healthAuthSite?.authorizedUser?.email
          // },
          ...ArrayUtils.insertIf(healthAuthSite?.healthAuthorityPharmanetAdministrator, {
            label: 'PharmaNet Administrator',
            email: healthAuthSite?.healthAuthorityPharmanetAdministrator?.email
          }),
          // TODO no privacy officer on health auth site view model
          // ...ArrayUtils.insertIf(healthAuthSite?.privacyOfficer.email, {
          //   label: 'Privacy Officer',
          //   email: healthAuthSite?.privacyOfficer.email
          // }),
          // TODO no technical support on health auth site view model
          // ...ArrayUtils.insertIf(healthAuthSite?.technicalSupport.email, {
          //   label: 'Technical Support Contact',
          //   email: healthAuthSite?.technicalSupport.email
          // })
        ]),
        catchError((error: any) => {
          this.toastService.openErrorToast('Health authority site contacts could not be retrieved');
          this.logger.error('[Core] HealthAuthorityResource::getHealthAuthoritySiteContacts error has occurred: ', error);
          throw error;
        })
      );
  }

  public updateHealthAuthoritySiteVendor(healthAuthId: number, siteId: number, payload: VendorForm): Observable<NoContent> {
    return this.apiResource.put<NoContent>(`health-authorities/${healthAuthId}/sites/${siteId}/vendor`, payload)
      .pipe(
        NoContentResponse,
        catchError((error: any) => {
          this.toastService.openErrorToast('Health authority site vendor could not be updated');
          this.logger.error('[Core] HealthAuthorityResource::updateHealthAuthoritySiteVendor error has occurred: ', error);
          throw error;
        })
      );
  }

  public updateHealthAuthoritySiteInfo(healthAuthCode: number, siteId: number, payload: SiteInformationForm): NoContent {
    return this.apiResource.put<NoContent>(`health-authorities/${healthAuthCode}/sites/${siteId}/site-info`, payload)
      .pipe(
        NoContentResponse,
        catchError((error: any) => {
          this.toastService.openErrorToast('Health authority site information could not be updated');
          this.logger.error('[Core] HealthAuthorityResource::updateHealthAuthoritySiteInfo error has occurred: ', error);
          throw error;
        })
      );
  }

  public updateHealthAuthoritySiteCareType(healthAuthId: number, siteId: number, payload: HealthAuthCareTypeForm): NoContent {
    return this.apiResource.put<NoContent>(`health-authorities/${healthAuthId}/sites/${siteId}/care-type`, payload)
      .pipe(
        NoContentResponse,
        catchError((error: any) => {
          this.toastService.openErrorToast('Health authority care type could not be updated');
          this.logger.error('[Core] HealthAuthorityResource::updateHealthAuthoritySiteCareType error has occurred: ', error);
          throw error;
        })
      );
  }

  public updateHealthAuthoritySitePhysicalAddress(healthAuthId: number, siteId: number, payload: SiteAddressForm): NoContent {
    return this.apiResource.put<NoContent>(`health-authorities/${healthAuthId}/sites/${siteId}/address`, payload.physicalAddress)
      .pipe(
        NoContentResponse,
        catchError((error: any) => {
          this.toastService.openErrorToast('Health authority site address could not be updated');
          this.logger.error('[Core] HealthAuthorityResource::updateHealthAuthoritySitePhysicalAddress error has occurred: ', error);
          throw error;
        })
      );
  }

  public updateHealthAuthoritySiteHoursOperation(healthAuthId: number, siteId: number, payload: HoursOperationForm): NoContent {
    return this.apiResource.put<HealthAuthority>(`health-authorities/${healthAuthId}/sites/${siteId}/hours-operation`, payload)
      .pipe(
        NoContentResponse,
        catchError((error: any) => {
          this.toastService.openErrorToast('Health authority hours of operation could not be updated');
          this.logger.error('[Core] HealthAuthorityResource::updateHealthAuthoritySiteHoursOperation error has occurred: ', error);
          throw error;
        })
      );
  }

  public updateHealthAuthoritySiteRemoteUsers(healthAuthId: number, siteId: number, payload: RemoteUsersForm): NoContent {
    return this.apiResource.put<HealthAuthority>(`health-authorities/${healthAuthId}/sites/${siteId}/remote-users`, payload)
      .pipe(
        NoContentResponse,
        catchError((error: any) => {
          this.toastService.openErrorToast('Health authority remote users could not be updated');
          this.logger.error('[Core] HealthAuthorityResource::updateHealthAuthoritySiteRemoteUsers error has occurred: ', error);
          throw error;
        })
      );
  }

  public updateHealthAuthorityPharmanetAdministrator(healthAuthId: number, siteId: number, payload: AdministratorForm): NoContent {
    return this.apiResource.put<HealthAuthority>(`health-authorities/${healthAuthId}/sites/${siteId}/administrator`, payload)
      .pipe(
        NoContentResponse,
        catchError((error: any) => {
          this.toastService.openErrorToast('Health authority administrator could not be updated');
          this.logger.error('[Core] HealthAuthorityResource::updateHealthAuthoritySiteAdministrator error has occurred: ', error);
          throw error;
        })
      );
  }

  /**
   * @description
   * Mark the as completed indicating the workflow has been entirely traversed
   * in wizard mode, and will now spoke between the views from overview.
   */
  public healthAuthoritySiteCompleted(healthAuthCode: number, siteId: number): NoContent {
    return this.apiResource.put<NoContent>(`health-authorities/${healthAuthCode}/sites/${siteId}/site-completed`)
      .pipe(
        NoContentResponse,
        catchError((error: any) => {
          this.toastService.openErrorToast('Health authority site could not be marked as completed');
          this.logger.error('[Core] HealthAuthorityResource::completed error has occurred: ', error);
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
