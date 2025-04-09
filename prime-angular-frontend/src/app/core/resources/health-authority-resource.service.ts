import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

import { Contact } from '@lib/models/contact.model';
import { HealthAuthorityEnum } from '@lib/enums/health-authority.enum';
import { PrivacyOffice } from '@lib/models/privacy-office.model';
import { ApiResource } from '@core/resources/api-resource.service';
import { ApiHttpResponse } from '@core/models/api-http-response.model';
import { ConsoleLoggerService } from '@core/services/console-logger.service';
import { NoContent, NoContentResponse } from '@core/resources/abstract-resource';
import { ToastService } from '@core/services/toast.service';
import { AuthorizedUser } from '@shared/models/authorized-user.model';
import { CapitalizePipe } from '@shared/pipes/capitalize.pipe';
import { HealthAuthority } from '@shared/models/health-authority.model';
import { HealthAuthorityRow } from '@shared/models/health-authority-row.model';
import { BaseDocument } from '@shared/components/document-upload/document-upload/document-upload.component';

import { HealthAuthoritySiteAdminList } from '@health-auth/shared/models/health-authority-admin-site-list.model';
import { HealthAuthoritySiteAdmin } from '@health-auth/shared/models/health-authority-admin-site.model';
import { ApiResourceUtilsService } from './api-resource-utils.service';
import { BusinessDay } from '@lib/models/business-day.model';

@Injectable({
  providedIn: 'root'
})
export class HealthAuthorityResource {
  constructor(
    private apiResource: ApiResource,
    private toastService: ToastService,
    private logger: ConsoleLoggerService,
    private capitalizePipe: CapitalizePipe,
    private apiResourceUtilsService: ApiResourceUtilsService,
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

  public getAllHealthAuthoritySites(): Observable<HealthAuthoritySiteAdminList[]> {
    return this.apiResource.get<HealthAuthoritySiteAdminList[]>(`health-authorities/sites`)
      .pipe(
        map((response: ApiHttpResponse<HealthAuthoritySiteAdminList[]>) => response.result),
        tap((healthAuthoritySites: HealthAuthoritySiteAdminList[]) => this.logger.info('HEALTH_AUTHORITY_SITES', healthAuthoritySites)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Health authority sites could not be retrieved');
          this.logger.error('[Core] HealthAuthorityResource::getAllHealthAuthoritySites error has occurred: ', error);
          throw error;
        })
      );
  }

  public getHealthAuthoritySitesByQuery(
    queryParam: {
      textSearch?: string, adjudicatorId?: number, vendorId?: number,
      careType?: string, statusId?: number, healthAuthorityId?: number, assignToMe: boolean
    }
  ): Observable<HealthAuthoritySiteAdminList[]> {
    const params = this.apiResourceUtilsService.makeHttpParams(queryParam);
    return this.apiResource.get<HealthAuthoritySiteAdminList[]>(`health-authorities/sites-query`, params)
      .pipe(
        map((response: ApiHttpResponse<HealthAuthoritySiteAdminList[]>) => response.result),
        tap((healthAuthoritySites: HealthAuthoritySiteAdminList[]) => this.logger.info('HEALTH_AUTHORITY_SITES', healthAuthoritySites)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Health authority sites could not be retrieved');
          this.logger.error('[Core] HealthAuthorityResource::getHealthAuthoritySitesByQuery error has occurred: ', error);
          throw error;
        })
      );
  }

  public getHealthAuthorityAdminSite(healthAuthorityId: number, siteId: number): Observable<HealthAuthoritySiteAdmin> {
    return this.apiResource.get<HealthAuthoritySiteAdmin>(`health-authorities/${healthAuthorityId}/sites/${siteId}/admin-view`)
      .pipe(
        map((response: ApiHttpResponse<HealthAuthoritySiteAdmin>) => response.result),
        // reformat the hours from API
        map((healthAuthoritySite: HealthAuthoritySiteAdmin) => {
          healthAuthoritySite.businessHours = healthAuthoritySite.businessHours.map((businessDay: BusinessDay) => {
            return BusinessDay.asHoursAndMins(businessDay);
          });
          return healthAuthoritySite;
        }),
        tap((healthAuthoritySite: HealthAuthoritySiteAdmin) => this.logger.info('HEALTH_AUTHORITY_SITE 2', healthAuthoritySite)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Health authority site could not be retrieved');
          this.logger.error('[Core] HealthAuthorityResource::getHealthAuthorityAdminSite error has occurred: ', error);
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
          this.logger.error('[Core] HealthAuthorityResource::updateHealthAuthorityCareTypes error has occurred: ', error);
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
          this.logger.error('[Core] HealthAuthorityResource::updateHealthAuthorityVendors error has occurred: ', error);
          throw error;
        })
      );
  }

  public getHealthAuthorityVendorSiteIds(healthAuthorityId: number, vendorId: number): Observable<number[]> {
    return this.apiResource.get<number[]>(`health-authorities/${healthAuthorityId}/vendors/${vendorId}/sites`)
      .pipe(
        map((response: ApiHttpResponse<number[]>) => response.result),
        tap((healthAuthoritySiteIds: number[]) => this.logger.info('HEALTH_AUTHORITY_SITES', healthAuthoritySiteIds)),
        catchError((error: any) => {
          this.logger.error('[Core] HealthAuthorityResource::getHealthAuthorityVendorSiteIds error has occurred: ', error);
          throw error;
        })
      );
  }

  public getHealthAuthorityCareTypeSiteIds(healthAuthorityId: number, careTypeId: number): Observable<number[]> {
    return this.apiResource.get<number[]>(`health-authorities/${healthAuthorityId}/care-types/${careTypeId}/sites`)
      .pipe(
        map((response: ApiHttpResponse<number[]>) => response.result),
        tap((healthAuthoritySiteIds: number[]) => this.logger.info('HEALTH_AUTHORITY_SITES', healthAuthoritySiteIds)),
        catchError((error: any) => {
          this.logger.error('[Core] HealthAuthorityResource::getHealthAuthorityCareTypeSiteIds error has occurred: ', error);
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
          this.logger.error('[Core] HealthAuthorityResource::updateHealthAuthorityPrivacyOffice error has occurred: ', error);
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

  public createOrganizationAgreementDocument(healthAuthorityId: number, documentGuid: string): Observable<BaseDocument> {
    const params = this.apiResourceUtilsService.makeHttpParams({ documentGuid });
    return this.apiResource.put<BaseDocument>(`health-authorities/${healthAuthorityId}/organization-agreement`, null, params)
      .pipe(
        map((response: ApiHttpResponse<BaseDocument>) => response.result),
        catchError((error: any) => {
          this.logger.error('[Core] HealthAuthorityResource::createOrganizationAgreementDocument error has occurred: ', error);
          throw error;
        })
      );
  }

  public getOrganizationAgreementDocumentToken(healthAuthorityId: number): Observable<string> {
    return this.apiResource.get<string>(`health-authorities/${healthAuthorityId}/organization-agreement/token`)
      .pipe(
        map((response: ApiHttpResponse<string>) => response.result),
        catchError((error: any) => {
          this.toastService.openErrorToast('Organization Agreement token could not be retrieved');
          this.logger.error('[Core] HealthAuthorityResource::getOrganizationAgreementDocumentToken error has occurred: ', error);
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


  /**
   * @description
   * Check health authority passcode, return true when health authority list is not empty
   */
  public checkHealthAuthorityPasscode(passcode: string): Observable<boolean> {
    const params = this.apiResourceUtilsService.makeHttpParams({ passcode });
    return this.apiResource.get<HealthAuthority[]>('lookups/ha-by-passcode', params)
      .pipe(
        map((response: ApiHttpResponse<HealthAuthority[]>) => response.result),
        map((healthAuthorityList: HealthAuthority[]) => {
          return healthAuthorityList.length > 0;
        }),
        catchError((error: any) => {
          this.toastService.openErrorToast(`Check Health authority Passcode - ${passcode}, error occurred.`);
          this.logger.error(`[Core] HealthAuthorityResource::checkHealthAuthorityPasscode(${passcode}) error has occurred: `, error);
          throw error;
        })
      );
  }
}
