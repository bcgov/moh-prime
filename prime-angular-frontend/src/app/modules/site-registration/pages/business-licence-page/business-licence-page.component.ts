import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormControl, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSlideToggle, MatSlideToggleChange } from '@angular/material/slide-toggle';

import { Observable } from 'rxjs';
import { exhaustMap } from 'rxjs/operators';

import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { SiteResource } from '@core/resources/site-resource.service';
import { UtilsService } from '@core/services/utils.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { NoContent } from '@core/resources/abstract-resource';
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

  public get businessLicenceGuid(): FormControl {
    return this.form.get('businessLicenceGuid') as FormControl;
  }

  public get deferredLicenceReason(): FormControl {
    return this.form.get('deferredLicenceReason') as FormControl;
  }

  public get doingBusinessAs(): FormControl {
    return this.form.get('doingBusinessAs') as FormControl;
  }

  public get site(): Site {
    return this.siteService.site;
  }

  public isCommPharm() {
    return this.siteService.site.careSettingCode === CareSettingEnum.COMMUNITY_PHARMACIST;
  }

  public onUpload(document: BaseDocument) {
    this.businessLicenceGuid.patchValue(document.documentGuid);
    this.uploadedFile = true;
    this.hasNoBusinessLicenceError = false;
  }

  public onRemoveDocument(documentGuid: string) {
    this.businessLicenceGuid.patchValue(null);
    this.uploadedFile = false;
  }

  public onBack() {
    this.routeUtils.routeRelativeTo(SiteRoutes.CARE_SETTING);
  }

  public downloadBusinessLicence(event: Event) {
    event.preventDefault();
    this.siteResource.getBusinessLicenceDocumentToken(this.siteService.site.id)
      .subscribe((token: string) =>
        this.utilsService.downloadToken(token)
      );
  }

  public onDeferredLicenceChange($event: MatSlideToggleChange) {
    if ($event.checked) {
      this.deferredLicenceReason.setValidators([Validators.required]);
      this.formUtilsService.resetAndClearValidators(this.doingBusinessAs);
      this.hasNoBusinessLicenceError = false;
      this.doingBusinessAs.disable();
      this.documentUpload.disable();
    } else {
      this.formUtilsService.resetAndClearValidators(this.deferredLicenceReason);
      this.doingBusinessAs.setValidators([Validators.required]);
      this.doingBusinessAs.enable();
      this.documentUpload.enable();
    }
    this.form.markAsUntouched();
  }

  public ngOnInit() {
    this.createFormInstance();
    this.patchForm();
    this.initForm();
  }

  protected createFormInstance() {
    this.formState = this.siteFormStateService.businessLicencePageFormState;
    this.form = this.formState.form;
  }

  protected patchForm(): void {
    const site = this.siteService.site;
    this.isCompleted = site?.completed;
    this.siteFormStateService.setForm(site, true);
    this.form.markAsPristine();
  }

  protected initForm(): void {
    const site = this.siteService.site;
    this.getBusinessLicence(site);
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

  protected performSubmission(): Observable<BusinessLicence | BusinessLicenceDocument> | NoContent {
    const siteId = this.route.snapshot.params.sid;
    const payload = this.siteFormStateService.json;
    let request$: Observable<BusinessLicence | BusinessLicenceDocument> | NoContent;

    if (this.deferredLicenceToggle?.checked) {
      this.businessLicence.deferredLicenceReason = payload.deferredLicenceReason;

      if (this.businessLicence.id) {
        // Update the business licence, and if a document already exists remove it
        const updateBusinessLicence$ = this.siteResource.updateBusinessLicence(siteId, this.businessLicence);
        request$ = (this.businessLicence.businessLicenceDocument)
          ? this.siteResource.removeBusinessLicenceDocument(siteId).pipe(exhaustMap(() => updateBusinessLicence$))
          : updateBusinessLicence$;
      }
      else {
        // Initial time through, and no business licence exists
        request$ = this.siteResource.createBusinessLicence(siteId, this.businessLicence, null);
      }
    }
    else {
      const updateSite$ = this.siteResource.updateSite(payload);

      if (this.businessLicence.id) {
        // Update the site, and if a document has been uploaded create a new business licence
        request$ = (this.uploadedFile)
          ? updateSite$.pipe(exhaustMap(() => this.siteResource.createBusinessLicenceDocument(siteId, payload.businessLicenceGuid)))
          : updateSite$;
      }
      else {
        // Initial time through and no business licence exists
        request$ = updateSite$.pipe(exhaustMap(() =>
          this.siteResource.createBusinessLicence(siteId, this.businessLicence, payload.businessLicenceGuid)));
      }
    }

    return request$;
  }

  protected afterSubmitIsSuccessful(): void {
    // Remove the business licence GUID to prevent 404 already
    // submitted if resubmited in same session
    this.businessLicenceGuid.patchValue(null);
    this.form.markAsPristine();

    const routePath = (this.isCompleted)
      ? SiteRoutes.SITE_REVIEW
      : SiteRoutes.SITE_ADDRESS;

    this.routeUtils.routeRelativeTo(routePath);
  }

  private getBusinessLicence(site: Site) {
    this.busy = this.siteResource.getBusinessLicence(site.id)
      .subscribe((businessLicense: BusinessLicence) => {
        this.businessLicence = businessLicense ?? this.businessLicence;
        if (businessLicense && !businessLicense.completed) {
          this.deferredLicenceToggle.checked = true;
          this.deferredLicenceReason.setValidators([Validators.required]);
          this.formUtilsService.resetAndClearValidators(this.doingBusinessAs);
          this.hasNoBusinessLicenceError = false;
          this.doingBusinessAs.disable();
          this.documentUpload.disable();
        }
      });
  }
}
