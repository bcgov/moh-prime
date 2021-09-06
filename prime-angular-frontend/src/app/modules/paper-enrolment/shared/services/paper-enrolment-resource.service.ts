import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

import { NoContent, NoContentResponse } from '@core/resources/abstract-resource';
import { ApiResource } from '@core/resources/api-resource.service';
import { ApiHttpResponse } from '@core/models/api-http-response.model';
import { ConsoleLoggerService } from '@core/services/console-logger.service';
import { ApiResourceUtilsService } from '@core/resources/api-resource-utils.service';
import { ToastService } from '@core/services/toast.service';
import { HttpEnrollee } from '@shared/models/enrolment.model';
import { EnrolmentCertificateAccessToken } from '@shared/models/enrolment-certificate-access-token.model';
import { SelfDeclaration } from '@shared/models/self-declarations.model';
// TODO move to lib
import { OboSite } from '@enrolment/shared/models/obo-site.model';
import { CollegeCertification } from '@enrolment/shared/models/college-certification.model';
import { EnrolleeAdjudicationDocument } from '@registration/shared/models/adjudication-document.model';

import { DemographicForm } from '@paper-enrolment/pages/demographic-page/demographic-form.model';
import { CareSettingForm } from '@paper-enrolment/pages/care-setting-page/care-setting-form.model';

@Injectable({
  providedIn: 'root'
})
export class PaperEnrolmentResource {
  constructor(
    private apiResource: ApiResource,
    private apiResourceUtilsService: ApiResourceUtilsService,
    private toastService: ToastService,
    private logger: ConsoleLoggerService
  ) { }

  public getEnrolleeById(enrolleeId: number): Observable<HttpEnrollee> {
    return this.apiResource.get<HttpEnrollee>(`enrollees/${enrolleeId}`)
      .pipe(
        map((response: ApiHttpResponse<HttpEnrollee>) => response.result),
        tap((enrollee: HttpEnrollee) => this.logger.info('ENROLLEE', enrollee)),
        catchError((error: any) => {
          this.logger.error('[Enrolment] PaperEnrolmentResource::getEnrolleeById error has occurred: ', error);
          throw error;
        })
      );
  }

  public createEnrollee(payload: DemographicForm): Observable<HttpEnrollee> {
    return this.apiResource.post<HttpEnrollee>('enrollees/paper-submissions', payload)
      .pipe(
        map((response: ApiHttpResponse<HttpEnrollee>) => response.result),
        tap((enrollee: HttpEnrollee) => this.logger.info('ENROLLEE', enrollee)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Enrollee could not be created.');
          this.logger.error('[Enrolment] PaperEnrolmentResource::createEnrollee error has occurred: ', error);
          throw error;
        })
      );
  }

  public updateDemographic(enrolleeId: number, demographic: DemographicForm): NoContent {
    return this.apiResource.put<NoContent>(`enrollees/${enrolleeId}/paper-submissions/demographics`, demographic)
      .pipe(
        NoContentResponse,
        catchError((error: any) => {
          this.toastService.openErrorToast('Paper Enrolment demographic could not be updated');
          this.logger.error('[Core] PaperEnrolmentResource::updateDemographic error has occurred: ', error);
          throw error;
        })
      );
  }

  public updateCareSettings(enrolleeId: number, careSettingForm: CareSettingForm): NoContent {
    return this.apiResource.put<NoContent>(`enrollees/${enrolleeId}/paper-submissions/care-settings`, careSettingForm)
      .pipe(
        NoContentResponse,
        catchError((error: any) => {
          this.toastService.openErrorToast('Paper Enrolment care settings could not be updated');
          this.logger.error('[Core] PaperEnrolmentResource::updateCareSettings error has occurred: ', error);
          throw error;
        })
      );
  }

  public updateCertifications(enrolleeId: number, certifications: CollegeCertification[]): NoContent {
    return this.apiResource.put<NoContent>(`enrollees/${enrolleeId}/paper-submissions/certifications`, certifications)
      .pipe(
        NoContentResponse,
        catchError((error: any) => {
          this.toastService.openErrorToast('Paper Enrolment certifications could not be updated');
          this.logger.error('[Core] PaperEnrolmentResource::updateCertifications error has occurred: ', error);
          throw error;
        })
      );
  }

  public updateOboSites(enrolleeId: number, oboSites: OboSite[]): NoContent {
    return this.apiResource.put<NoContent>(`enrollees/${enrolleeId}/paper-submissions/obo-sites`, oboSites)
      .pipe(
        NoContentResponse,
        catchError((error: any) => {
          this.toastService.openErrorToast('Paper Enrolment obo sites could not be updated');
          this.logger.error('[Core] PaperEnrolmentResource::updateOboSites error has occurred: ', error);
          throw error;
        })
      );
  }

  public updateSelfDeclarations(enrolleeId: number, selfDeclarations: SelfDeclaration[]): NoContent {
    return this.apiResource.put<NoContent>(`enrollees/${enrolleeId}/paper-submissions/self-declarations`, selfDeclarations)
      .pipe(
        NoContentResponse,
        catchError((error: any) => {
          this.toastService.openErrorToast('Paper Enrolment self declarations could not be updated');
          this.logger.error('[Core] PaperEnrolmentResource::updateSelfDeclarations error has occurred: ', error);
          throw error;
        })
      );
  }

  public updateAgreementType(enrolleeId: number, agreementType: number): NoContent {
    return this.apiResource.put<NoContent>(`enrollees/${enrolleeId}/paper-submissions/agreement`, { agreementType })
      .pipe(
        NoContentResponse,
        catchError((error: any) => {
          this.toastService.openErrorToast('Paper Enrolment agreement could not be updated');
          this.logger.error('[Core] PaperEnrolmentResource::updateAgreementType error has occurred: ', error);
          throw error;
        })
      );
  }

  /**
   * @description
   * Mark the as completed indicating the workflow has been entirely traversed
   * in wizard mode, and will now spoke between the views from overview.
   */
  public profileCompleted(enrolleeId: number): NoContent {
    return this.apiResource.put<NoContent>(`enrollees/${enrolleeId}/paper-submissions/profile-completed`)
      .pipe(
        NoContentResponse,
        catchError((error: any) => {
          this.toastService.openErrorToast('Paper Enrolment could not be marked as completed');
          this.logger.error('[Core] PaperEnrolmentResource::profileCompleted error has occurred: ', error);
          throw error;
        })
      );
  }

  /**
   * @description
   * Finalize the paper enrolment submission.
   */
  public finalize(enrolleeId: number): NoContent {
    return this.apiResource.post<NoContent>(`enrollees/${enrolleeId}/paper-submissions/finalize`)
      .pipe(
        NoContentResponse,
        catchError((error: any) => {
          this.toastService.openErrorToast('Paper Enrolment could not be finalized');
          this.logger.error('[Core] PaperEnrolmentResource::finalize error has occurred: ', error);
          throw error;
        })
      );
  }

  public sendProvisionerAccessLink(
    emails: string = null, enrolleeId: number, careSettingCode: number
  ): Observable<EnrolmentCertificateAccessToken> {
    const payload = { data: emails };
    return this.apiResource
      .post<EnrolmentCertificateAccessToken>(`enrollees/${enrolleeId}/provisioner-access/send-link/${careSettingCode}`, payload)
      .pipe(
        map((response: ApiHttpResponse<EnrolmentCertificateAccessToken>) => response.result),
        tap((token: EnrolmentCertificateAccessToken) => this.logger.info('ACCESS_TOKEN', token)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Email could not be sent');
          this.logger.error('[Core] PaperEnrolmentResource::sendProvisionerAccessLink error has occurred: ', error);
          throw error;
        })
      );
  }

  public getEnrolleeAdjudicationDocumentDownloadToken(enrolleeId: number, documentId: number): Observable<string> {
    return this.apiResource.get<string>(`enrollees/${enrolleeId}/adjudication-documents/${documentId}`)
      .pipe(
        map((response: ApiHttpResponse<string>) => response.result),
        catchError((error: any) => {
          this.logger.error('[Enrolment] PaperEnrolmentResource::getEnrolleeAdjudicationDocumentDownloadToken error has occurred: ',
            error);
          throw error;
        })
      );
  }


  public updateAdjudicationDocuments(enrolleeId: number, documentsGuidAndType: { documentGuid, documentType }[]): Observable<NoContent> {
    return this.apiResource.put<NoContent>(`enrollees/${enrolleeId}/paper-submissions/documents`, documentsGuidAndType)
      .pipe(
        NoContentResponse,
        catchError((error: any) => {
          this.toastService.openErrorToast('Paper Enrolment documents could not be updated');
          this.logger.error('[Core] PaperEnrolmentResource::updateAdjudicationDocuments error has occurred: ', error);
          throw error;
        })
      );
  }

  public getAdjudicationDocuments(enrolleeId: number): Observable<EnrolleeAdjudicationDocument[]> {
    return this.apiResource.get<EnrolleeAdjudicationDocument[]>(`enrollees/${enrolleeId}/paper-submissions/documents`)
      .pipe(
        map((response: ApiHttpResponse<EnrolleeAdjudicationDocument[]>) => response.result),
        catchError((error: any) => {
          this.logger.error('[Enrolment] EnrolmentResource::getAdjudicationDocuments error has occurred: ', error);
          throw error;
        })
      );
  }
}
