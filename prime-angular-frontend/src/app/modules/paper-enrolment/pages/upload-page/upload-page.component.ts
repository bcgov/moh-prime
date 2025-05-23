import { Component, OnInit } from '@angular/core';
import { UntypedFormBuilder } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';

import { of } from 'rxjs';
import { exhaustMap } from 'rxjs/operators';

import { EnumUtils } from '@lib/utils/enum-utils.class';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { UtilsService } from '@core/services/utils.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { NoContent } from '@core/resources/abstract-resource';
import { PaperEnrolmentAgreementType, PaperEnrolmentAgreementTypeNameMap } from '@shared/enums/agreement-type.enum';
import { DocumentType } from '@shared/enums/document-type.enum';
import { BaseDocument } from '@shared/components/document-upload/document-upload/document-upload.component';
import { HttpEnrollee } from '@shared/models/enrolment.model';
import { EnrolleeAdjudicationDocument } from '@registration/shared/models/adjudication-document.model';

import { PaperEnrolmentRoutes } from '@paper-enrolment/paper-enrolment.routes';
import { PaperEnrolmentResource } from '@paper-enrolment/shared/services/paper-enrolment-resource.service';
import { UploadFormState } from './upload-form-state.class';

@Component({
  selector: 'app-upload-page',
  templateUrl: './upload-page.component.html',
  styleUrls: ['./upload-page.component.scss']
})
export class UploadPageComponent extends AbstractEnrolmentPage implements OnInit {
  public formState: UploadFormState;
  public agreementTypes: number[];
  public savedDocuments: EnrolleeAdjudicationDocument[];
  public PaperEnrolmentAgreementTypeNameMap = PaperEnrolmentAgreementTypeNameMap;
  public hasNoUploadError: boolean;

  private routeUtils: RouteUtils;
  private documents: Pick<BaseDocument, 'documentGuid' | 'documentType'>[]

  constructor(
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService,
    private fb: UntypedFormBuilder,
    private paperEnrolmentResource: PaperEnrolmentResource,
    private utilsService: UtilsService,
    private route: ActivatedRoute,
    router: Router
  ) {
    super(dialog, formUtilsService);

    this.documents = [];
    this.agreementTypes = EnumUtils.values(PaperEnrolmentAgreementType);
    this.routeUtils = new RouteUtils(route, router, PaperEnrolmentRoutes.MODULE_PATH);
  }

  public onUpload({ documentGuid }: BaseDocument, componentName: string = ''): void {
    var documentType = Object.values(DocumentType).includes(componentName)
      ? DocumentType[componentName]
      : DocumentType['NoType'];

    this.documents.push({ documentGuid, documentType });

    this.hasNoUploadError = false;
  }

  public getDocument(documentId: number): void {
    const enrolleeId = +this.route.snapshot.params.eid;
    this.paperEnrolmentResource.getEnrolleeAdjudicationDocumentDownloadToken(enrolleeId, documentId)
      .subscribe((token: string) =>
        this.utilsService.downloadToken(token)
      );
  }

  public onBack() {
    this.routeUtils.routeRelativeTo([PaperEnrolmentRoutes.SELF_DECLARATION]);
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.patchForm();
  }

  protected createFormInstance(): void {
    this.formState = new UploadFormState(this.fb);
  }

  protected patchForm(): void {
    const enrolleeId = +this.route.snapshot.params.eid;
    if (!enrolleeId) {
      throw new Error('No enrollee ID was provided');
    }

    this.paperEnrolmentResource.getEnrolleeById(enrolleeId)
      .subscribe(({ assignedTOAType }: HttpEnrollee) => {
        if (assignedTOAType) {
          this.formState.patchValue({ assignedTOAType });
        }
      });

    this.paperEnrolmentResource.getAdjudicationDocuments(enrolleeId)
      .subscribe(documents => {
        this.savedDocuments = documents;
        documents.forEach(({ documentGuid, documentType }) => {
          this.documents.push({ documentGuid, documentType });
        });
      })
  }

  protected additionalValidityChecks(): boolean {
    return !!this.documents.length;
  }

  protected performSubmission(): NoContent {
    this.formState.form.markAsPristine();

    const enrolleeId = +this.route.snapshot.params.eid;
    return this.paperEnrolmentResource.updateAgreementType(enrolleeId, this.formState.json.assignedTOAType)
      .pipe(
        exhaustMap(() =>
          (this.documents.length)
            ? this.paperEnrolmentResource.updateAdjudicationDocuments(enrolleeId, this.documents)
            : of(null)
        ),
        exhaustMap(() => this.paperEnrolmentResource.profileCompleted(enrolleeId))
      );
  }

  protected onSubmitFormIsValid(): void {
    this.hasNoUploadError = false;
  }

  protected onSubmitFormIsInvalid(): void {
    if (!this.documents.length) {
      this.hasNoUploadError = true;
    }
  }

  protected afterSubmitIsSuccessful(enrolleeId: number) {
    this.routeUtils.routeRelativeTo([PaperEnrolmentRoutes.OVERVIEW]);
  }
}
