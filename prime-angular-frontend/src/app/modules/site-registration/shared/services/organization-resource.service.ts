import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';
import { map, catchError, tap } from 'rxjs/operators';

import { ApiResource } from '@core/resources/api-resource.service';
import { ApiResourceUtilsService } from '@core/resources/api-resource-utils.service';
import { LoggerService } from '@core/services/logger.service';
import { ApiHttpResponse } from '@core/models/api-http-response.model';
import { ToastService } from '@core/services/toast.service';
import { NoContent } from '@core/resources/abstract-resource';

import { Organization } from '@registration/shared/models/organization.model';
import { Party } from '@registration/shared/models/party.model';

// TODO use ApiResourceUtils to build URLs
// TODO split out log messages for reuse into ErrorHandler
@Injectable({
  providedIn: 'root'
})
export class OrganizationResource {
  constructor(
    private apiResource: ApiResource,
    private apiResourceUtilsService: ApiResourceUtilsService,
    private toastService: ToastService,
    private logger: LoggerService
  ) { }

  public getOrganizations(): Observable<Organization[]> {
    return this.apiResource.get<Organization[]>('organizations')
      .pipe(
        map((response: ApiHttpResponse<Organization[]>) => response.result),
        tap((organizations: Organization[]) => this.logger.info('ORGANIZATIONS', organizations)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Organizations could not be retrieved');
          this.logger.error('[SiteRegistration] OrganizationResource::getOrganizations error has occurred: ', error);
          throw error;
        })
      );
  }

  public getOrganizationById(organizationId: number): Observable<Organization> {
    return this.apiResource.get<Organization>(`organizations/${organizationId}`)
      .pipe(
        map((response: ApiHttpResponse<Organization>) => response.result),
        tap((organization: Organization) => this.logger.info('ORGANIZATION', organization)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Organization could not be retrieved');
          this.logger.error('[SiteRegistration] OrganizationResource::getOrganizationById error has occurred: ', error);
          throw error;
        })
      );
  }

  public createOrganization(party: Party): Observable<Organization> {
    return this.apiResource.post<Organization>('organizations', party)
      .pipe(
        map((response: ApiHttpResponse<Organization>) => response.result),
        tap((newOrganization: Organization) => {
          this.toastService.openSuccessToast('Organization has been created');
          this.logger.info('NEW_ORGANIZATION', newOrganization);
        }),
        catchError((error: any) => {
          this.toastService.openErrorToast('Organization could not be created');
          this.logger.error('[SiteRegistration] OrganizationResource::createOrganization error has occurred: ', error);
          throw error;
        })
      );
  }

  public updateOrganization(organization: Organization, isCompleted?: boolean): NoContent {
    const params = this.apiResourceUtilsService.makeHttpParams({ isCompleted });
    return this.apiResource.put<NoContent>(`organizations/${organization.id}`, organization, params)
      // TODO remove pipe when ApiResource handles NoContent
      .pipe(
        map(() => {
          this.toastService.openSuccessToast('Organization has been updated');
        }),
        catchError((error: any) => {
          this.toastService.openErrorToast('Organization could not be updated');
          this.logger.error('[SiteRegistration] OrganizationResource::updateOrganization error has occurred: ', error);
          throw error;
        })
      );
  }

  public deleteOrganization(organizationId: number): Observable<Organization> {
    return this.apiResource.delete<Organization>(`organizations/${organizationId}`)
      .pipe(
        map((response: ApiHttpResponse<Organization>) => response.result),
        tap((organization: Organization) => {
          this.toastService.openSuccessToast('Organization has been deleted');
          this.logger.info('DELETED_ORGANIZATION', organization);
        }),
        catchError((error: any) => {
          this.toastService.openErrorToast('Organization could not be deleted');
          this.logger.error('[SiteRegistration] OrganizationResource::deleteOrganization error has occurred: ', error);
          throw error;
        })
      );
  }

  public submitOrganization(organization: Organization): Observable<string> {
    return this.apiResource.post<string>(`organizations/${organization.id}/submission`)
      .pipe(
        map((response: ApiHttpResponse<string>) => response.result),
        tap(() => this.toastService.openSuccessToast('Organization has been submitted')),
        catchError((error: any) => {
          this.toastService.openErrorToast('Organization could not be submitted');
          this.logger.error('[SiteRegistration] OrganizationResource::submitOrganization error has occurred: ', error);
          throw error;
        })
      );
  }

  public getOrganizationAgreement(organizationId: number): Observable<string> {
    return this.apiResource.get<string>(`organizations/${organizationId}/organization-agreement`)
      .pipe(
        map((response: ApiHttpResponse<string>) => response.result),
        catchError((error: any) => {
          this.toastService.openErrorToast('Organization agreement could not be retrieved');
          this.logger.error('[SiteRegistration] OrganizationResource::getOrganizationAgreement error has occurred: ', error);
          throw error;
        })
      );
  }

  public acceptCurrentOrganizationAgreement(organizationId: number): NoContent {
    return this.apiResource.put<NoContent>(`organizations/${organizationId}/organization-agreement`)
      .pipe(
        map(() => {
          this.toastService.openSuccessToast('Organization agreement has been accepted');
        }),
        catchError((error: any) => {
          this.toastService.openErrorToast('Organization agreement could not be accepted');
          this.logger.error('[SiteRegistration] OrganizationResource::acceptOrganizationAgreement error has occurred: ', error);
          throw error;
        })
      );
  }

  public getSignedOrganizationAgreement(organizationId: number): Observable<string> {
    return this.apiResource.get<string>(`organizations/${organizationId}/organization-agreement`)
      .pipe(
        map((response: ApiHttpResponse<string>) => response.result),
        catchError((error: any) => {
          this.toastService.openErrorToast('Organization agreement could not be retrieved');
          this.logger.error('[SiteRegistration] OrganizationResource::getCurrentOrganizationAgreement error has occurred: ', error);
          throw error;
        })
      );
  }

  public addSignedAgreement(organizationId: number, documentGuid: string, fileName: string): Observable<string> {
    const params = this.apiResourceUtilsService.makeHttpParams({ documentGuid, fileName });
    return this.apiResource.post<string>(`organizations/${organizationId}/signed-agreement`, { organizationId }, params)
      .pipe(
        map((response: ApiHttpResponse<string>) => response.result),
        tap(() => this.toastService.openSuccessToast('Signed agreement has been added')),
        catchError((error: any) => {
          this.toastService.openErrorToast('Signed agreement could not be added');
          this.logger.error('[SiteRegistration] SiteRegistrationResource::addSignedAgreement error has occurred: ', error);
          throw error;
        })
      );
  }
}
