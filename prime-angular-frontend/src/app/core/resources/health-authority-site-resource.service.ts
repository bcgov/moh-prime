import { Injectable } from '@angular/core';
import { HttpStatusCode } from '@angular/common/http';


import { forkJoin, Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

import { BusinessDay } from '@lib/models/business-day.model';
import { HealthAuthorityEnum } from '@lib/enums/health-authority.enum';
import { RemoteUser } from '@lib/models/remote-user.model';
import { ApiHttpResponse } from '@core/models/api-http-response.model';
import { ApiResource } from '@core/resources/api-resource.service';
import { ConsoleLoggerService } from '@core/services/console-logger.service';
import { NoContent, NoContentResponse } from '@core/resources/abstract-resource';
import { ToastService } from '@core/services/toast.service';

import { HealthAuthoritySite, HealthAuthoritySiteDto } from '@health-auth/shared/models/health-authority-site.model';
import { HealthAuthoritySiteCreate } from '@health-auth/shared/models/health-authority-site-create.model';
import { HealthAuthoritySiteUpdate } from '@health-auth/shared/models/health-authority-site-update.model';

@Injectable({
  providedIn: 'root'
})
export class HealthAuthoritySiteResource {
  constructor(
    private apiResource: ApiResource,
    private toastService: ToastService,
    private logger: ConsoleLoggerService
  ) { }

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

  // TODO doesn't contain business hours or remote users and will need typing adjusted
  public getHealthAuthoritySites(healthAuthId: HealthAuthorityEnum): Observable<HealthAuthoritySite[]> {
    return this.apiResource.get<HealthAuthoritySite[]>(`health-authorities/${healthAuthId}/sites`)
      .pipe(
        map((response: ApiHttpResponse<HealthAuthoritySite[]>) => response.result),
        map((healthAuthoritySiteDtos: HealthAuthoritySiteDto[]) =>
          healthAuthoritySiteDtos.map(hasd => HealthAuthoritySite.toHealthAuthoritySite(hasd))
        ),
        tap((healthAuthoritySites: HealthAuthoritySite[]) => this.logger.info('HEALTH_AUTHORITY_SITES', healthAuthoritySites)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Health authority sites could not be retrieved');
          this.logger.error('[Core] HealthAuthorityResource::getHealthAuthoritySites error has occurred: ', error);
          throw error;
        })
      );
  }

  public getHealthAuthoritySiteById(healthAuthId: HealthAuthorityEnum, healthAuthSiteId: number): Observable<HealthAuthoritySite | null> {
    const path = `health-authorities/${healthAuthId}/sites/${healthAuthSiteId}`;
    return forkJoin({
      healthAuthoritySite: this.apiResource.get<Omit<HealthAuthoritySiteDto, 'businessHours' | 'remoteUsers'>>(`${path}`, null, null, true),
      // TODO convert to hours and minutes in view model and drop this adapter
      businessHours: this.apiResource.get<BusinessDay[]>(`${path}/hours-operation`, null, null, true)
        .pipe(map((businessHours: BusinessDay[]) =>
          businessHours.map((businessDay: BusinessDay) => BusinessDay.asHoursAndMins(businessDay))
        )),
      remoteUsers: this.apiResource.get<RemoteUser[]>(`${path}/remote-users`, null, null, true)
    })
      .pipe(
        map(({
          healthAuthoritySite,
          businessHours,
          remoteUsers
        }: { healthAuthoritySite: HealthAuthoritySite, businessHours: BusinessDay[], remoteUsers: RemoteUser[] }) => {
          return { ...healthAuthoritySite, businessHours, remoteUsers };
        }),
        map((healthAuthoritySiteDto: HealthAuthoritySiteDto) => HealthAuthoritySite.toHealthAuthoritySite(healthAuthoritySiteDto)),
        tap((healthAuthoritySite: HealthAuthoritySite) => this.logger.info('HEALTH_AUTHORITY_SITE', healthAuthoritySite)),
        catchError((error: any) => {
          if (error.status === HttpStatusCode.NotFound) {
            return of(null);
          }

          this.toastService.openErrorToast('Health authority site could not be retrieved');
          this.logger.error('[Core] HealthAuthorityResource::getHealthAuthoritySiteById error has occurred: ', error);
          throw error;
        })
      );
  }

  // TODO [BREAKING CHANGE] in site-registration-tabs.component.ts L203 onNotify
  public getHealthAuthoritySiteContacts(healthAuthId: HealthAuthorityEnum, healthAuthSiteId: number): Observable<{ label: string, email: string }[]> {
    return this.getHealthAuthoritySiteById(healthAuthId, healthAuthSiteId)
      .pipe(
        map((healthAuthSite: HealthAuthoritySite) => [
          // TODO what needs to happen and what is available or different endpoint
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

  public updateHealthAuthoritySite(healthAuthId: HealthAuthorityEnum, siteId: number, updatedModel: HealthAuthoritySiteUpdate): NoContent {
    updatedModel.businessHours = updatedModel.businessHours
      .map((businessDay: BusinessDay) => BusinessDay.asTimespan(businessDay));
    return this.apiResource.put<NoContent>(`health-authorities/${healthAuthId}/sites/${siteId}`, updatedModel)
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
   public healthAuthoritySiteSubmit(healthAuthCode: number, siteId: number, updatedModel: HealthAuthoritySiteUpdate): NoContent {
    updatedModel.businessHours = updatedModel.businessHours
      .map((businessDay: BusinessDay) => BusinessDay.asTimespan(businessDay));
    return this.apiResource.post<NoContent>(`health-authorities/${healthAuthCode}/sites/${siteId}/submissions`, updatedModel)
      .pipe(
        NoContentResponse,
        catchError((error: any) => {
          this.toastService.openErrorToast('Health authority site could not be submitted');
          this.logger.error('[Core] HealthAuthorityResource::healthAuthoritySiteSubmit error has occurred: ', error);
          throw error;
        })
      );
  }

}
