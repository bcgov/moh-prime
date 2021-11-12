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

import { HealthAuthoritySite } from '@health-auth/shared/models/health-authority-site.model';
import { HealthAuthoritySiteAdminList } from '@health-auth/shared/models/health-authority-site-list.model';

@Injectable({
  providedIn: 'root'
})
export class HealthAuthorityResource {
  constructor(
    private apiResource: ApiResource,
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
