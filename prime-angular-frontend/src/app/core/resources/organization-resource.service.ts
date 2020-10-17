import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';
import { map, catchError, tap } from 'rxjs/operators';

import { ApiResource } from '@core/resources/api-resource.service';
import { ApiResourceUtilsService } from '@core/resources/api-resource-utils.service';
import { LoggerService } from '@core/services/logger.service';
import { ApiHttpResponse } from '@core/models/api-http-response.model';
import { ToastService } from '@core/services/toast.service';
import { NoContent, NoContentResponse } from '@core/resources/abstract-resource';
import { OrganizationAgreement, OrganizationAgreementViewModel } from '@shared/models/agreement.model';

import { Organization, OrganizationListViewModel } from '@registration/shared/models/organization.model';
import { Party } from '@registration/shared/models/party.model';

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

  public getOrganizations(): Observable<OrganizationListViewModel[]>;
  public getOrganizations(queryParams: { verbose: boolean }): Observable<OrganizationListViewModel[] | Organization[]>;
  public getOrganizations(queryParams: { verbose: boolean } = null): Observable<OrganizationListViewModel[] | Organization[]> {
    const params = this.apiResourceUtilsService.makeHttpParams(queryParams);
    return this.apiResource.get<OrganizationListViewModel[] | Organization[]>('organizations', params)
      .pipe(
        map((response: ApiHttpResponse<OrganizationListViewModel[] | Organization[]>) => response.result),
        tap((organizations: OrganizationListViewModel[] | Organization[]) => this.logger.info('ORGANIZATIONS', organizations)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Organizations could not be retrieved');
          this.logger.error('[Core] OrganizationResource::getOrganizations error has occurred: ', error);
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
          this.logger.error('[Core] OrganizationResource::getOrganizationById error has occurred: ', error);
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
          this.logger.error('[Core] OrganizationResource::createOrganization error has occurred: ', error);
          throw error;
        })
      );
  }

  public updateOrganization(organization: Organization): NoContent {
    return this.apiResource.put<NoContent>(`organizations/${organization.id}`, organization)
      .pipe(
        NoContentResponse,
        tap(() => this.toastService.openSuccessToast('Organization has been updated')),
        catchError((error: any) => {
          this.toastService.openErrorToast('Organization could not be updated');
          this.logger.error('[Core] OrganizationResource::updateOrganization error has occurred: ', error);
          throw error;
        })
      );
  }

  public updateCompleted(organizationId: number): NoContent {
    return this.apiResource.put<NoContent>(`organizations/${organizationId}/completed`)
      .pipe(
        NoContentResponse,
        catchError((error: any) => {
          this.logger.error('[Core] OrganizationResource::updateCompleted error has occurred: ', error);
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
          this.logger.error('[Core] OrganizationResource::deleteOrganization error has occurred: ', error);
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
          this.logger.error('[Core] OrganizationResource::submitOrganization error has occurred: ', error);
          throw error;
        })
      );
  }

  /**
   * @description
   * Check whether an organization agreement is needed, and create
   * the organization agreement
   */
  public updateOrganizationAgreement(organizationId: number, siteId: number): Observable<OrganizationAgreement | NoContent> {
    const params = this.apiResourceUtilsService.makeHttpParams({ siteId });
    return this.apiResource.get<OrganizationAgreement | NoContent>(`organizations/${organizationId}/agreements/update`, params)
      .pipe(
        map((response: ApiHttpResponse<OrganizationAgreement | NoContent>) => response?.result),
        tap((organizationAgreement: OrganizationAgreement) => this.logger.info('ORGANIZATION_AGREEMENT', organizationAgreement)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Organization agreement could not be updated');
          this.logger.error('[Core] OrganizationResource::updateOrganizationAgreement error has occurred: ', error);
          throw error;
        })
      );
  }

  /**
   * @description
   * Get a list of organization agreements.
   */
  public getOrganizationAgreements(organizationId: number): Observable<OrganizationAgreementViewModel[]> {
    return this.apiResource.get<OrganizationAgreementViewModel[] | NoContent>(`organizations/${organizationId}/agreements`)
      .pipe(
        map((response: ApiHttpResponse<OrganizationAgreementViewModel[]>) => response.result),
        tap((organizationAgreements: OrganizationAgreementViewModel[]) =>
          this.logger.info('ORGANIZATION_AGREEMENTS', organizationAgreements)
        ),
        catchError((error: any) => {
          this.toastService.openErrorToast('Organization agreement(s) could not be retrieved');
          this.logger.error('[Core] OrganizationResource::getOrganizationAgreements error has occurred: ', error);
          throw error;
        })
      );
  }

  /**
   * @description
   * Get an organization agreement.
   *
   * TODO WIP PRIME-1085
   * Unsigned (Both us markup or Base64 same key go nuts)
   * - HTML for rendering for electronic signature for each type
   * - Download PDF for signing (wet) for upload for each type (Base64 from csHtmlPDF)
   * Signed
   * - Get signed electronically signed agreement (HTML or PDF = csHtmlPDF)
   *   - Markup or Base64 in same key (same as unsigned)
   * - Get document GUID for signed agreement for download via token (always PDF)
   *   - GUID used to get Token to download
   */
  public getOrganizationAgreement(
    organizationId: number,
    agreementId: number,
    asPdf?: boolean // TODO should we include format?
  ): Observable<OrganizationAgreementViewModel> {
    const params = this.apiResourceUtilsService.makeHttpParams({ asPdf });
    return this.apiResource.get<OrganizationAgreementViewModel>(`organizations/${organizationId}/agreements/${agreementId}`, params)
      .pipe(
        map((response: ApiHttpResponse<OrganizationAgreementViewModel>) => response.result),
        tap((organizationAgreement: OrganizationAgreementViewModel) => this.logger.info('ORGANIZATION_AGREEMENT', organizationAgreement)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Organization agreement could not be retrieved');
          this.logger.error('[Core] OrganizationResource::getOrganizationAgreement error has occurred: ', error);
          throw error;
        })
      );
  }

  /**
   * @description
   * Get a organization agreement for signing as HTML markup for inline
   * display, or Base64 (PDF) for downloading.
   */
  // TODO is this accurate?
  public getOrganizationAgreementForSigning(organizationId: number, agreementId: number) {
    return this.apiResource.get<OrganizationAgreement>(`organizations/${organizationId}/agreements/${agreementId}/signable`)
      .pipe(
        map((response: ApiHttpResponse<OrganizationAgreement>) => response.result),
        tap((organizationAgreement: OrganizationAgreement) => this.logger.info('ORGANIZATION_AGREEMENT_SIGNABLE', organizationAgreement)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Organization agreement could not be retrieved');
          this.logger.error('[Core] OrganizationResource::getOrganizationAgreementForSigning error has occurred: ', error);
          throw error;
        })
      );
  }

  /**
   * @description
   * Get a download token for a signed organization agreement.
   */
  // TODO create helper pipe to download the PDF using utils service
  public getSignedOrganizationAgreementToken(organizationId: number, agreementId: number): Observable<string> {
    return this.apiResource.get<string>(`organizations/${organizationId}/agreements/${agreementId}/signed`)
      .pipe(
        map((response: ApiHttpResponse<string>) => response.result),
        tap((token: string) => this.logger.info('ORGANIZATION_AGREEMENT_SIGNED', token)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Organization agreement token could not be retrieved');
          this.logger.error('[Core] OrganizationResource::getSignedOrganizationAgreement error has occurred: ', error);
          throw error;
        })
      );
  }

  public acceptOrganizationAgreement(organizationId: number, agreementId: number, organizationAgreementGuid: string): NoContent {
    const params = this.apiResourceUtilsService.makeHttpParams({ organizationAgreementGuid });
    return this.apiResource.put<NoContent>(`organizations/${organizationId}/agreements/${agreementId}`, null, params)
      .pipe(
        NoContentResponse,
        tap(() => this.toastService.openSuccessToast('Organization agreement has been accepted')),
        catchError((error: any) => {
          this.toastService.openErrorToast('Organization agreement could not be accepted');
          this.logger.error('[Core] OrganizationResource::acceptOrganizationAgreement error has occurred: ', error);
          throw error;
        })
      );
  }
}
