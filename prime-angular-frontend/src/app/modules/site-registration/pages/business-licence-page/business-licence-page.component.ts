import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSlideToggle, MatSlideToggleChange } from '@angular/material/slide-toggle';

import { Observable, concat } from 'rxjs';

import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { SiteResource } from '@core/resources/site-resource.service';
import { UtilsService } from '@core/services/utils.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { BaseDocument, DocumentUploadComponent } from '@shared/components/document-upload/document-upload/document-upload.component';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';

import { SiteRoutes } from '@registration/site-registration.routes';
import { Site } from '@registration/shared/models/site.model';
import { BusinessLicenceDocument } from '@registration/shared/models/business-licence-document.model';
import { SiteService } from '@registration/shared/services/site.service';
import { SiteFormStateService } from '@registration/shared/services/site-form-state.service';
import { BusinessLicence } from '@registration/shared/models/business-licence.model';
import { BusinessLicencePageFormState } from './business-licence-page-form-state.class';

@Component({
  selector: 'app-business-licence-page',
  templateUrl: './business-licence-page.component.html',
  styleUrls: ['./business-licence-page.component.scss']
})
export class BusinessLicencePageComponent extends AbstractEnrolmentPage implements OnInit {
  public formState: BusinessLicencePageFormState;
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
    private siteService: SiteService,
    private siteFormStateService: SiteFormStateService,
    private siteResource: SiteResource,
    private utilsService: UtilsService,
    private route: ActivatedRoute,
    router: Router
  ) {
    super(dialog, formUtilsService);

    this.title = this.route.snapshot.data.title;
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.MODULE_PATH);
    this.uploadedFile = false;

    this.businessLicenceDocuments = [];
    this.businessLicence = new BusinessLicence(this.siteService.site.id);
  }

  public isCommPharm(): boolean {
    return this.siteService.site.careSettingCode === CareSettingEnum.COMMUNITY_PHARMACIST;
  }

  public onUpload(document: BaseDocument): void {
    this.formState.businessLicenceGuid.patchValue(document.documentGuid);
    this.uploadedFile = true;
    this.hasNoBusinessLicenceError = false;
  }

  public onRemoveDocument(documentGuid: string): void {
    this.formState.businessLicenceGuid.patchValue(null);
    this.uploadedFile = false;
  }

  public onBack(): void {
    this.routeUtils.routeRelativeTo(SiteRoutes.CARE_SETTING);
  }

  public downloadBusinessLicence(event: Event): void {
    event.preventDefault();
    this.siteResource.getBusinessLicenceDocumentToken(this.siteService.site.id)
      .subscribe((token: string) => this.utilsService.downloadToken(token));
  }

  public onDeferredLicenceChange(event: MatSlideToggleChange): void {
    if (event.checked) {
      this.formState.deferredLicenceReason.setValidators([Validators.required]);
      this.formUtilsService.resetAndClearValidators(this.formState.doingBusinessAs);
      this.hasNoBusinessLicenceError = false;
      this.formState.doingBusinessAs.disable();
      this.documentUpload.disable();
    } else {
      this.formUtilsService.resetAndClearValidators(this.formState.deferredLicenceReason);
      this.formState.doingBusinessAs.setValidators([Validators.required]);
      this.formState.doingBusinessAs.enable();
      this.documentUpload.enable();
    }
    this.formState.form.markAsUntouched();
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.patchForm();
    this.initForm();
  }

  protected createFormInstance(): void {
    this.formState = this.siteFormStateService.businessLicencePageFormState;
  }

  protected patchForm(): void {
    const site = this.siteService.site;
    this.isCompleted = site?.completed;
    this.siteFormStateService.setForm(site, true);
    this.formState.form.markAsPristine();
  }

  protected initForm(): void {
    this.site = this.siteService.site;
    this.getBusinessLicence(this.site);
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

  protected performSubmission(): Observable<BusinessLicence | BusinessLicenceDocument | void> {
    // Collect a list of requests that will be executed in order, which
    // will always update the site initially
    const requests$: Observable<BusinessLicence | BusinessLicenceDocument | void>[] = [
      this.siteResource.updateSite(this.siteFormStateService.json)
    ];
    const siteId = this.route.snapshot.params.sid;
    const hasBusinessLicence = !!this.businessLicence.id;

    // Create or update the business licence with an uploaded document, otherwise
    // with a deferred licence reason
    if (this.deferredLicenceToggle.checked) {
      this.businessLicence.deferredLicenceReason = this.formState.deferredLicenceReason.value;

      if (!hasBusinessLicence) {
        requests$.push(this.siteResource.createBusinessLicence(siteId, this.businessLicence, null));
      } else {
        // Perform an update by removing an existing business licence document
        // and/or update the deferred reason
        if (this.businessLicence.businessLicenceDocument) {
          requests$.push(this.siteResource.removeBusinessLicenceDocument(siteId));
        }
        requests$.push(this.siteResource.updateBusinessLicence(siteId, this.businessLicence));
      }
    } else {
      const businessLicenceGuid = this.formState.businessLicenceGuid.value;

      if (!hasBusinessLicence) {
        requests$.push(this.siteResource.createBusinessLicence(siteId, this.businessLicence, businessLicenceGuid));
      } else if (this.uploadedFile) {
        requests$.push(this.siteResource.createBusinessLicenceDocument(siteId, businessLicenceGuid));
      }
    }

    return concat(...requests$);
  }

  protected afterSubmitIsSuccessful(): void {
    // Remove the business licence GUID to prevent 404 already
    // submitted if re-submitted in same session
    this.formState.businessLicenceGuid.patchValue(null);
    this.formState.form.markAsPristine();

    const routePath = (this.isCompleted)
      ? SiteRoutes.SITE_REVIEW
      : SiteRoutes.SITE_ADDRESS;

    this.routeUtils.routeRelativeTo(routePath);
  }

  private getBusinessLicence(site: Site): void {
    this.siteResource.getBusinessLicence(site.id)
      .subscribe((businessLicense: BusinessLicence) => {
        this.businessLicence = businessLicense ?? this.businessLicence;
        if (businessLicense && !businessLicense.completed) {
          this.deferredLicenceToggle.checked = !!this.businessLicence.deferredLicenceReason;
          this.formState.deferredLicenceReason.setValidators([Validators.required]);
          this.formUtilsService.resetAndClearValidators(this.formState.doingBusinessAs);
          this.hasNoBusinessLicenceError = false;
          this.formState.doingBusinessAs.disable();
          this.documentUpload.disable();
        }
      });
  }
}
