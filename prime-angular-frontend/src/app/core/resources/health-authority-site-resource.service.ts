import { Injectable } from '@angular/core';
import { HttpStatusCode } from '@angular/common/http';

import { forkJoin, Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

import { BusinessDay } from '@lib/models/business-day.model';
import { HealthAuthorityEnum } from '@lib/enums/health-authority.enum';
import { ApiHttpResponse } from '@core/models/api-http-response.model';
import { ApiResource } from '@core/resources/api-resource.service';
import { ConsoleLoggerService } from '@core/services/console-logger.service';
import { NoContent, NoContentResponse } from '@core/resources/abstract-resource';
import { ToastService } from '@core/services/toast.service';
import { ApiResourceUtilsService } from './api-resource-utils.service';

import { HealthAuthoritySite, HealthAuthoritySiteDto } from '@health-auth/shared/models/health-authority-site.model';
import { HealthAuthoritySiteCreate } from '@health-auth/shared/models/health-authority-site-create.model';
import { HealthAuthoritySiteUpdate } from '@health-auth/shared/models/health-authority-site-update.model';
import { HealthAuthoritySiteAdminList } from '@health-auth/shared/models/health-authority-admin-site-list.model';
import { ArrayUtils } from '@lib/utils/array-utils.class';
import { HealthAuthority } from '@shared/models/health-authority.model';

@Injectable({
  providedIn: 'root'
})
export class HealthAuthoritySiteResource {
  constructor(
    private apiResource: ApiResource,
    private toastService: ToastService,
    private logger: ConsoleLoggerService,
    private apiResourceUtilsService: ApiResourceUtilsService
  ) { }

  public createHealthAuthoritySite(healthAuthId: HealthAuthorityEnum, createModel: HealthAuthoritySiteCreate): Observable<HealthAuthoritySite> {
    return this.apiResource.post<HealthAuthoritySite>(`health-authorities/${healthAuthId}/sites`, createModel)
      .pipe(
        map((response: ApiHttpResponse<HealthAuthoritySite>) => response.result),
        tap((healthAuthoritySite: HealthAuthoritySite) => this.logger.info('HEALTH_AUTH_SITE', healthAuthoritySite)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Health authority site could not be created');
          this.logger.error('[Core] HealthAuthoritySiteResource::createHealthAuthoritySite error has occurred: ', error);
          throw error;
        })
      );
  }

  public getHealthAuthorityAdminSites(healthAuthId: HealthAuthorityEnum, healthAuthoritySiteId: number = null): Observable<HealthAuthoritySiteAdminList[]> {
    const params = this.apiResourceUtilsService.makeHttpParams({ healthAuthoritySiteId });
    return this.apiResource.get<HealthAuthoritySiteAdminList[]>(`health-authorities/${healthAuthId}/sites`, params)
      .pipe(
        map((response: ApiHttpResponse<HealthAuthoritySiteAdminList[]>) => response.result),
        tap((healthAuthoritySites: HealthAuthoritySiteAdminList[]) => this.logger.info('HEALTH_AUTHORITY_SITES', healthAuthoritySites)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Health authority sites could not be retrieved');
          this.logger.error('[Core] HealthAuthoritySiteResource::getHealthAuthoritySites error has occurred: ', error);
          throw error;
        })
      );
  }

  public getHealthAuthoritySiteById(healthAuthId: HealthAuthorityEnum, healthAuthSiteId: number): Observable<HealthAuthoritySite | null> {
    const path = `health-authorities/${healthAuthId}/sites/${healthAuthSiteId}`;
    return forkJoin({
      healthAuthoritySite: this.apiResource.get<Omit<HealthAuthoritySiteDto, 'businessHours'>>(`${path}`, null, null, true),
      // TODO convert to hours and minutes in view model and drop this adapter
      businessHours: this.apiResource.get<BusinessDay[]>(`${path}/hours-operation`, null, null, true)
        .pipe(map((businessHours: BusinessDay[]) =>
          businessHours.map((businessDay: BusinessDay) => BusinessDay.asHoursAndMins(businessDay))
        ))
    })
      .pipe(
        map(({
          healthAuthoritySite,
          businessHours
        }: { healthAuthoritySite: HealthAuthoritySite, businessHours: BusinessDay[] }) => {
          return { ...healthAuthoritySite, businessHours };
        }),
        map((healthAuthoritySiteDto: HealthAuthoritySiteDto) => HealthAuthoritySite.toHealthAuthoritySite(healthAuthoritySiteDto)),
        tap((healthAuthoritySite: HealthAuthoritySite) => this.logger.info('HEALTH_AUTHORITY_SITE 1', healthAuthoritySite)),
        catchError((error: any) => {
          if (error.status === HttpStatusCode.NotFound) {
            return of(null);
          }

          this.toastService.openErrorToast('Health authority site could not be retrieved');
          this.logger.error('[Core] HealthAuthoritySiteResource::getHealthAuthoritySiteById error has occurred: ', error);
          throw error;
        })
      );
  }

  // TODO [BREAKING CHANGE] in site-registration-tabs.component.ts L203 onNotify
  public getHealthAuthoritySiteContacts(healthAuthId: HealthAuthorityEnum, healthAuthSiteId: number): Observable<{ label: string, email: string }[]> {
    const path = `health-authorities/${healthAuthId}/sites/${healthAuthSiteId}`;
    return forkJoin({
      healthAuthority: this.apiResource.get<HealthAuthority>(`health-authorities/${healthAuthId}`, null, null, true),
      healthAuthoritySite: this.apiResource.get<Omit<HealthAuthoritySiteDto, 'businessHours'>>(`${path}`, null, null, true),
    })
      .pipe(
        map(({
          healthAuthority,
          healthAuthoritySite
        }: { healthAuthority: HealthAuthority, healthAuthoritySite: HealthAuthoritySite }) => {
          return [
            ...ArrayUtils.insertIf(healthAuthoritySite?.healthAuthorityPharmanetAdministratorId, {
              label: 'PharmaNet Administrator',
              email: healthAuthority?.pharmanetAdministrators.find(a => a.id === healthAuthoritySite?.healthAuthorityPharmanetAdministratorId)?.email
            }),
            ...ArrayUtils.insertIf(healthAuthoritySite?.healthAuthorityTechnicalSupportId, {
              label: 'Technical Support Contact',
              email: healthAuthority?.technicalSupports.find(s => s.id === healthAuthoritySite?.healthAuthorityTechnicalSupportId)?.email
            }),
            ...ArrayUtils.insertIf(healthAuthority?.privacyOffice?.privacyOfficer, {
              label: 'Privacy Officer Contact',
              email: healthAuthority?.privacyOffice?.privacyOfficer?.email
            })];
        }),
        catchError((error: any) => {
          this.toastService.openErrorToast('Health authority site contacts could not be retrieved');
          this.logger.error('[Core] HealthAuthoritySiteResource::getHealthAuthoritySiteContacts error has occurred: ', error);
          throw error;
        })
      );
  }

  public updateHealthAuthoritySite(healthAuthId: HealthAuthorityEnum, siteId: number, updatedModel: HealthAuthoritySiteUpdate): NoContent {
    updatedModel.businessHours = updatedModel.businessHours
      .map((businessDay: BusinessDay) => BusinessDay.asTimespan(businessDay));
    return this.apiResource.put<NoContent>(`health-authorities/${healthAuthId}/sites/${siteId}`, updatedModel)
      .pipe(
        NoContentResponse,
        catchError((error: any) => {
          this.toastService.openErrorToast('Health authority site could not be updated');
          this.logger.error('[Core] HealthAuthoritySiteResource::updateHealthAuthoritySite error has occurred: ', error);
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
          this.logger.error('[Core] HealthAuthoritySiteResource::setHealthAuthoritySiteCompleted error has occurred: ', error);
          throw error;
        })
      );
  }

  /**
   * @description
   * Submit the health authority site registration.
   */
  public healthAuthoritySiteSubmit(healthAuthCode: number, siteId: number, updatedModel: HealthAuthoritySiteUpdate): NoContent {
    updatedModel.businessHours = updatedModel.businessHours
      .map((businessDay: BusinessDay) => BusinessDay.asTimespan(businessDay));
    return this.apiResource.post<NoContent>(`health-authorities/${healthAuthCode}/sites/${siteId}/submissions`, updatedModel)
      .pipe(
        NoContentResponse,
        catchError((error: any) => {
          this.toastService.openErrorToast('Health authority site could not be submitted');
          this.logger.error('[Core] HealthAuthoritySiteResource::healthAuthoritySiteSubmit error has occurred: ', error);
          throw error;
        })
      );
  }


  /**
   * @description
   * Transfer the health authority site(s) from one authorized user to another.
   */
  public transferHealthAuthoritySite(healthAuthCode: number, currentAuthorizedUserId: number, newAuthorizedUserId: number): NoContent {
    return this.apiResource.post<NoContent>(`health-authorities/${healthAuthCode}/sites/transfer/from/${currentAuthorizedUserId}/to/${newAuthorizedUserId}`)
      .pipe(
        NoContentResponse,
        catchError((error: any) => {
          this.toastService.openErrorToast('Health authority site could not be transfered');
          this.logger.error('[Core] HealthAuthoritySiteResource::transferHealthAuthoritySite error has occurred: ', error);
          throw error;
        })
      );
  }
}
