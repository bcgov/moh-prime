import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormControl, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSlideToggle, MatSlideToggleChange } from '@angular/material/slide-toggle';

import { Observable, concat } from 'rxjs';

import { ArrayUtils } from '@lib/utils/array-utils.class';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { SiteResource } from '@core/resources/site-resource.service';
import { UtilsService } from '@core/services/utils.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { BaseDocument, DocumentUploadComponent } from '@shared/components/document-upload/document-upload/document-upload.component';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';

import { AbstractCommunitySiteRegistrationPage } from '@registration/shared/classes/abstract-community-site-registration-page.class';
import { SiteRoutes } from '@registration/site-registration.routes';
import { Site } from '@registration/shared/models/site.model';
import { BusinessLicenceDocument } from '@registration/shared/models/business-licence-document.model';
import { SiteService } from '@registration/shared/services/site.service';
import { SiteFormStateService } from '@registration/shared/services/site-form-state.service';
import { BusinessLicence } from '@registration/shared/models/business-licence.model';
import { BusinessLicenceFormState } from './business-licence-form-state.class';

// TODO refactor business licence pages into a single page
@Component({
  selector: 'app-business-licence-page',
  templateUrl: './business-licence-page.component.html',
  styleUrls: ['./business-licence-page.component.scss']
})
export class BusinessLicencePageComponent extends AbstractCommunitySiteRegistrationPage implements OnInit {
  public formState: BusinessLicenceFormState;
  public title: string;
  public routeUtils: RouteUtils;
  public businessLicence: BusinessLicence;
  public businessLicenceDocuments: BusinessLicenceDocument[];
  public uploadedFile: boolean;
  public hasNoBusinessLicenceError: boolean;
  public isCompleted: boolean;
  public SiteRoutes = SiteRoutes;
  public site: Site;

  @ViewChild('deferredLicence') public deferredLicenceToggle: MatSlideToggle;
  @ViewChild('documentUpload') public documentUpload: DocumentUploadComponent;

  constructor(
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService,
    protected siteService: SiteService,
    protected siteFormStateService: SiteFormStateService,
    protected siteResource: SiteResource,
    private utilsService: UtilsService,
    private route: ActivatedRoute,
    router: Router
  ) {
    super(dialog, formUtilsService, siteService, siteFormStateService, siteResource);

    this.title = route.snapshot.data.title;
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.MODULE_PATH);
    this.uploadedFile = false;

    this.businessLicenceDocuments = [];
    this.businessLicence = new BusinessLicence(this.siteService.site.id);
  }

  public canDefer(): boolean {
    return [
      CareSettingEnum.COMMUNITY_PHARMACIST,
      CareSettingEnum.DEVICE_PROVIDER
    ].includes(this.siteService.site.careSettingCode);
  }

  public onUpload(document: BaseDocument): void {
    this.formState.businessLicenceGuid.patchValue(document.documentGuid);
    this.uploadedFile = true;
    this.hasNoBusinessLicenceError = false;
  }

  public onRemoveDocument(_: string): void {
    this.formState.businessLicenceGuid.patchValue(null);
    this.uploadedFile = false;
  }

  public onBack(): void {
    const nextRoute = (!this.isCompleted)
      ? SiteRoutes.CARE_SETTING
      : SiteRoutes.SITE_REVIEW;

    this.routeUtils.routeRelativeTo(nextRoute);
  }

  public downloadBusinessLicence(event: Event): void {
    event.preventDefault();
    this.siteResource.getBusinessLicenceDocumentToken(this.siteService.site.id, this.siteService.site.businessLicence.id)
      .subscribe((token: string) => this.utilsService.downloadToken(token));
  }

  public onDeferredLicenceChange(event: MatSlideToggleChange): void {
    this.updateBusLicAccess(event.checked);
    this.formState.form.markAsUntouched();
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.patchForm();
    this.initForm();
  }

  protected createFormInstance(): void {
    this.formState = this.siteFormStateService.businessLicenceFormState;
  }

  protected patchForm(): void {
    const site = this.siteService.site;
    this.isCompleted = site?.completed;
    this.siteFormStateService.setForm(site, !this.hasBeenSubmitted);
    this.formState.form.markAsPristine();
  }

  protected initForm(): void {
    this.site = this.siteService.site;
    this.getBusinessLicence(this.site.id);
  }

  protected additionalValidityChecks(): boolean {
    return this.uploadedFile || this.deferredLicenceToggle?.checked || !!this.businessLicence?.businessLicenceDocument;
  }

  protected onSubmitFormIsValid(): void {
    this.hasNoBusinessLicenceError = false;
  }

  protected onSubmitFormIsInvalid(): void {
    if (!this.uploadedFile && !this.deferredLicenceToggle?.checked && !this.businessLicence?.businessLicenceDocument) {
      this.hasNoBusinessLicenceError = true;
    }
  }

  // TODO: Put Dialog in her
  protected submissionRequest(): Observable<BusinessLicence | BusinessLicenceDocument | void> {
    const siteId = this.route.snapshot.params.sid;
    const currentBusinessLicence = this.siteService.site.businessLicence;
    const updatedBusinessLicence = this.siteFormStateService.businessLicenceFormState.json.businessLicence;
    const documentGuid = this.siteFormStateService.businessLicenceFormState.businessLicenceGuid.value;

    return concat(
      this.siteResource.updateSite(this.siteFormStateService.json),
      ...this.businessLicenceUpdates(
        siteId,
        currentBusinessLicence,
        updatedBusinessLicence,
        documentGuid
      )
    );
  }

  protected afterSubmitIsSuccessful(): void {
    const routePath = (this.isCompleted)
      ? SiteRoutes.SITE_REVIEW
      : SiteRoutes.SITE_ADDRESS;

    this.routeUtils.routeRelativeTo(routePath);
  }

  private getBusinessLicence(siteId: number): void {
    this.siteResource.getBusinessLicence(siteId)
      .subscribe((businessLicense: BusinessLicence) => {
        this.businessLicence = businessLicense ?? this.businessLicence;

        if (businessLicense && !businessLicense.completed) {
          const canDefer = this.canDefer();

          // Business licence may exist, but the deferred licence toggle may be
          // hidden based on care setting leaving the toggle undefined
          if (canDefer) {
            this.deferredLicenceToggle.checked = !!this.businessLicence.deferredLicenceReason;
          }

          this.updateBusLicAccess(canDefer);
        }
      });
  }

  private updateBusLicAccess(toggleAccess: boolean): void {
    let enableOrDisable: 'enable' | 'disable';

    if (toggleAccess) {
      enableOrDisable = 'disable';
      this.updateBusLicValidations(
        [this.formState.deferredLicenceReason],
        [this.formState.doingBusinessAs, this.formState.businessLicenceExpiry]
      );
    } else {
      enableOrDisable = 'enable';
      this.updateBusLicValidations(
        [this.formState.doingBusinessAs, this.formState.businessLicenceExpiry],
        [this.formState.deferredLicenceReason]
      );
    }

    this.formState.doingBusinessAs[enableOrDisable]();
    (!this.businessLicence.deferredLicenceReason)
      ? this.documentUpload.enable()
      : this.documentUpload[enableOrDisable]();
  }

  private updateBusLicValidations(requiredControls: FormControl[], notRequiredControls: FormControl[]): void {
    this.hasNoBusinessLicenceError = false;
    requiredControls.forEach(control => control.setValidators([Validators.required]));
    notRequiredControls.forEach(control => this.formUtilsService.resetAndClearValidators(control));
  }

  /**
   * @description
   * Collect a list of requests that will be executed in order, which
   * will update the business licence directly during initial registration
   * and then becomes the responsibility of site submission.
   */
  public businessLicenceUpdates(
    siteId: number,
    currentBusinessLicence: BusinessLicence,
    updatedBusinessLicence: BusinessLicence,
    documentGuid: string = null
  ): Observable<BusinessLicence | BusinessLicenceDocument | void>[] {
    if (!currentBusinessLicence?.id) {
      // Create a business licence when none existed
      return [this.siteResource.createBusinessLicence(siteId, updatedBusinessLicence, documentGuid)];
    }

    updatedBusinessLicence.id = currentBusinessLicence.id;

    if (currentBusinessLicence.deferredLicenceReason !== updatedBusinessLicence.deferredLicenceReason) {
      // Remove an existing business licence document before updating
      // with a reason for deferment
      return [
        ...ArrayUtils.insertResultIf(
          currentBusinessLicence?.businessLicenceDocument,
          () => [this.siteResource.removeBusinessLicenceDocument(siteId, currentBusinessLicence.id)]
        ),
        this.siteResource.updateBusinessLicence(siteId, updatedBusinessLicence)
      ];
    }

    // Create a business licence document each time a file is uploaded, and
    // update the existing business licence
    return [
      ...ArrayUtils.insertResultIf(
        documentGuid,
        () => [
          this.siteResource.removeBusinessLicenceDocument(siteId, currentBusinessLicence.id),
          this.siteResource.createBusinessLicenceDocument(siteId, currentBusinessLicence.id, documentGuid)
        ]
      ),
      this.siteResource.updateBusinessLicence(siteId, updatedBusinessLicence)
    ];
  }
}
