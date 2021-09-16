import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatCheckbox } from '@angular/material/checkbox';
import { MatDialog } from '@angular/material/dialog';

import { EMPTY, noop, of } from 'rxjs';
import { exhaustMap, map } from 'rxjs/operators';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { OrganizationResource } from '@core/resources/organization-resource.service';
import { UtilsService } from '@core/services/utils.service';
import { SiteResource } from '@core/resources/site-resource.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { NoContent } from '@core/resources/abstract-resource';
import { BaseDocument } from '@shared/components/document-upload/document-upload/document-upload.component';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { OrganizationAgreement, OrganizationAgreementViewModel } from '@shared/models/agreement.model';
import { AgreementType } from '@shared/enums/agreement-type.enum';

import { SiteRoutes } from '@registration/site-registration.routes';
import { OrganizationFormStateService } from '@registration/shared/services/organization-form-state.service';
import { OrganizationService } from '@registration/shared/services/organization.service';
import { OrganizationAgreementPageFormState } from './organization-agreement-page-form-state.class';
import { SiteService } from '@registration/shared/services/site.service';

@Component({
  selector: 'app-organization-agreement-page',
  templateUrl: './organization-agreement-page.component.html',
  styleUrls: ['./organization-agreement-page.component.scss']
})
export class OrganizationAgreementPageComponent extends AbstractEnrolmentPage implements OnInit {
  public formState: OrganizationAgreementPageFormState;
  public routeUtils: RouteUtils;
  public agreementId: number;
  public organizationAgreement: OrganizationAgreementViewModel;
  public hasDownloadedFile: boolean;
  public hasUploadedFile: boolean;
  public hasNoUploadError: boolean;
  public isCompleted: boolean;
  public SiteRoutes = SiteRoutes;

  @ViewChild('accept') public accepted: MatCheckbox;

  constructor(
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService,
    private organizationService: OrganizationService,
    private siteService: SiteService,
    private organizationResource: OrganizationResource,
    private organizationFormStateService: OrganizationFormStateService,
    private siteResource: SiteResource,
    private utilsService: UtilsService,
    private route: ActivatedRoute,
    router: Router
  ) {
    super(dialog, formUtilsService);

    this.organizationAgreement = null;
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.MODULE_PATH);
  }

  public onDownload() {
    const agreementType = this.organizationAgreement.agreementType;
    if (
      AgreementType.COMMUNITY_PRACTICE_ORGANIZATION_AGREEMENT !== agreementType &&
      AgreementType.COMMUNITY_PHARMACY_ORGANIZATION_AGREEMENT !== agreementType
    ) {
      return;
    }

    this.busy = this.organizationResource
      .getOrganizationAgreementForSigning(this.route.snapshot.params.oid, agreementType)
      .subscribe((organizationAgreement: string) => {
        const blob = this.utilsService.base64ToBlob(organizationAgreement);
        this.utilsService.downloadDocument(blob, 'Organization-Agreement');
        this.hasDownloadedFile = true;
      });
  }

  public onUpload(document: BaseDocument) {
    this.formState.organizationAgreementGuid.patchValue(document.documentGuid);
    this.hasUploadedFile = true;
    this.hasNoUploadError = false;
  }

  public onRemoveDocument(documentGuid: string) {
    this.formState.organizationAgreementGuid.patchValue(null);
  }

  public showDefaultAgreement() {
    return !this.organizationService.organization.hasAcceptedAgreement;
  }

  public onBack() {
    this.routeUtils.routeRelativeTo(SiteRoutes.TECHNICAL_SUPPORT);
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.patchForm();
    this.initForm();
  }

  protected createFormInstance() {
    this.formState = this.organizationFormStateService.organizationAgreementPageFormState;
  }

  protected patchForm(): void {
    const organization = this.organizationService.organization;
    this.isCompleted = organization?.completed;
    this.organizationFormStateService.setForm(organization);
  }

  protected initForm() {
    const organization = this.organizationService.organization;
    const careSettingCode = this.siteService.site.careSettingCode;
    this.busy = this.organizationResource
      .updateOrganizationAgreement(organization.id, careSettingCode)
      .pipe(
        map(({ id }: OrganizationAgreement) =>
          this.agreementId = id
        ),
        exhaustMap((agreementId: number) =>
          this.organizationResource.getOrganizationAgreement(organization.id, agreementId)
        )
      )
      .subscribe((organizationAgreement: OrganizationAgreementViewModel) =>
        this.organizationAgreement = organizationAgreement
      );
  }

  protected checkValidity(): boolean {
    return this.accepted?.checked || this.hasUploadedFile;
  }

  protected performSubmission(): NoContent {
    const organizationId = this.route.snapshot.params.oid;
    const data: DialogOptions = {
      title: 'Organization Agreement',
      message: 'Are you sure you want to accept the Organization Agreement?',
      actionText: 'Accept Organization Agreement'
    };

    return this.dialog.open(ConfirmDialogComponent, { data })
      .afterClosed()
      .pipe(
        exhaustMap((result: boolean) =>
          (result)
            ? of(noop)
            : EMPTY
        ),
        exhaustMap(() =>
          (this.formState.organizationAgreementGuid.value)
            ? this.organizationResource
              .acceptOrganizationAgreement(organizationId, this.agreementId, this.formState.organizationAgreementGuid.value)
            : this.organizationResource
              .acceptOrganizationAgreement(organizationId, this.agreementId)
        ),
        exhaustMap(() => this.siteResource.setSiteCompleted((this.route.snapshot.params.sid)))
      );
  }

  protected afterSubmitIsSuccessful(): void {
    // Remove the org agreement GUID to prevent 404 already
    // submitted if resubmitted in the same session
    this.formState.organizationAgreementGuid.patchValue(null);
    this.formState.form.markAsPristine();

    this.routeUtils.routeRelativeTo(SiteRoutes.SITE_REVIEW);
  }
}
