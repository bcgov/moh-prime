import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';

import { of, Subscription } from 'rxjs';
import { exhaustMap } from 'rxjs/operators';

import { EnumUtils } from '@lib/utils/enum-utils.class';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { UtilsService } from '@core/services/utils.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { NoContent } from '@core/resources/abstract-resource';
import { AgreementType, AgreementTypeNameMap } from '@shared/enums/agreement-type.enum';
import { BaseDocument } from '@shared/components/document-upload/document-upload/document-upload.component';
import { HttpEnrollee } from '@shared/models/enrolment.model';
import { EnrolleeAdjudicationDocument } from '@registration/shared/models/adjudication-document.model';

import { PaperEnrolmentRoutes } from '@paper-enrolment/paper-enrolment.routes';
import { PaperEnrolmentResource } from '@paper-enrolment/services/paper-enrolment-resource.service';
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
  public AgreementTypeNameMap = AgreementTypeNameMap;

  private routeUtils: RouteUtils;
  private documentGuids: string[];

  constructor(
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService,
    private fb: FormBuilder,
    private paperEnrolmentResource: PaperEnrolmentResource,
    private utilsService: UtilsService,
    private route: ActivatedRoute,
    router: Router
  ) {
    super(dialog, formUtilsService);

    this.documentGuids = [];
    this.agreementTypes = EnumUtils.values(AgreementType);
    this.routeUtils = new RouteUtils(route, router, PaperEnrolmentRoutes.MODULE_PATH);
  }

  public onUpload(document: BaseDocument): void {
    this.documentGuids.push(document.documentGuid);
  }

  public onRemoveDocument(document: BaseDocument): void {
    if (this.documentGuids.includes(document.documentGuid)) {
      this.documentGuids = this.documentGuids.filter(i => i !== document.documentGuid);
    }
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
      return;
    }

    this.paperEnrolmentResource.getEnrolleeById(enrolleeId)
      .subscribe(({ assignedTOAType }: HttpEnrollee) => {
        if (assignedTOAType) {
          this.formState.patchValue({ agreementType: assignedTOAType });
        }
      });

    this.paperEnrolmentResource.getAdjudicationDocuments(enrolleeId)
      .subscribe(documents => this.savedDocuments = documents);
  }

  protected performSubmission(): NoContent {
    this.formState.form.markAsPristine();

    const enrolleeId = +this.route.snapshot.params.eid;
    return this.paperEnrolmentResource.updateAgreementType(enrolleeId, this.formState.json.agreementType)
      .pipe(
        exhaustMap(() =>
          (this.documentGuids.length > 0)
            ? this.paperEnrolmentResource.updateAdjudicationDocuments(enrolleeId, this.documentGuids)
            : of(null)
        ),
        exhaustMap(() => this.paperEnrolmentResource.profileCompleted(enrolleeId))
      );
  }

  protected afterSubmitIsSuccessful(enrolleeId: number) {
    this.routeUtils.routeRelativeTo([PaperEnrolmentRoutes.OVERVIEW]);
  }
}
