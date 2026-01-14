import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UntypedFormControl, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSlideToggle, MatSlideToggleChange } from '@angular/material/slide-toggle';

import { Observable, concat, EMPTY, pipe } from 'rxjs';
import { exhaustMap, tap } from 'rxjs/operators';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { SiteResource } from '@core/resources/site-resource.service';
import { UtilsService } from '@core/services/utils.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { BaseDocument, DocumentUploadComponent } from '@shared/components/document-upload/document-upload/document-upload.component';
import { FormControlValidators } from '@lib/validators/form-control.validators';

import { AbstractCommunitySiteRegistrationPage } from '@registration/shared/classes/abstract-community-site-registration-page.class';
import { SiteRoutes } from '@registration/site-registration.routes';
import { Site } from '@registration/shared/models/site.model';
import { AddressLine } from '@lib/models/address.model';
import { BusinessLicenceDocument } from '@registration/shared/models/business-licence-document.model';
import { SiteService } from '@registration/shared/services/site.service';
import { SiteFormStateService } from '@registration/shared/services/site-form-state.service';
import { BusinessLicence } from '@registration/shared/models/business-licence.model';
import { BusinessLicenceFormState } from './business-licence-form-state.class';
import { APP_CONFIG, AppConfig } from 'app/app-config.module';

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
  public isSubmitted: boolean;
  public showAddressFields: boolean;
  public showExpiryDate: boolean;
  public formControlNames: AddressLine[];
  public SiteRoutes = SiteRoutes;
  public site: Site;

  @ViewChild('deferredLicence') public deferredLicenceToggle: MatSlideToggle;
  @ViewChild('documentUpload') public documentUpload: DocumentUploadComponent;

  constructor(
    @Inject(APP_CONFIG) public config: AppConfig,
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

    this.formControlNames = [
      'street',
      'city',
      'provinceCode',
      'postal'
    ];
  }

  public canDefer(): boolean {
    return [
      CareSettingEnum.COMMUNITY_PHARMACY,
      CareSettingEnum.DEVICE_PROVIDER,
      CareSettingEnum.PRIVATE_COMMUNITY_HEALTH_PRACTICE,
    ].includes(this.siteService.site.careSettingCode);
  }

  public isCommunityPharmacy(): boolean {
    return this.siteService.site.careSettingCode === CareSettingEnum.COMMUNITY_PHARMACY;
  }

  public isDeviceProvider(): boolean {
    return this.siteService.site.careSettingCode === CareSettingEnum.DEVICE_PROVIDER;
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
    this.isSubmitted = site?.submittedDate ? true : false;
    this.siteFormStateService.setForm(site, !this.hasBeenSubmitted);
    this.formState.form.markAsPristine();
    if (site.doingBusinessAs && site.businessLicence && site.businessLicence.expiryDate === null) {
      this.showExpiryDate = false;
    } else {
      //this.formState.businessLicenceExpiry.patchValue(site.businessLicence.expiryDate);
      this.showExpiryDate = true;
    }
  }

  protected initForm(): void {
    this.site = this.siteService.site;
    this.getBusinessLicence(this.site.id);
    if (this.site.careSettingCode === CareSettingEnum.COMMUNITY_PHARMACY) {
      if (this.site.activeBeforeRegistration) {
        this.formUtilsService.setValidators(this.formState.pec, [Validators.required, FormControlValidators.communityPharmacySiteId])
      } else {
        this.formUtilsService.setValidators(this.formState.pec, [FormControlValidators.communityPharmacySiteId])
      }
    } else {
      this.formUtilsService.setValidators(this.formState.pec, []);
    }
  }

  protected additionalValidityChecks(): boolean {
    return this.uploadedFile || this.deferredLicenceToggle?.checked || !!this.businessLicence?.businessLicenceDocument;
  }

  protected onSubmitFormIsValid(): void {
    this.hasNoBusinessLicenceError = false;
    this.showAddressFields = true;
  }

  protected onSubmitFormIsInvalid(): void {
    if (!this.uploadedFile && !this.deferredLicenceToggle?.checked && !this.businessLicence?.businessLicenceDocument && this) {
      this.hasNoBusinessLicenceError = true;
    }
    this.showAddressFields = true;
  }

  protected submissionRequest(): Observable<BusinessLicence | BusinessLicenceDocument | void> {
    const siteId = this.route.snapshot.params.sid;
    const currentBusinessLicence = this.siteService.site.businessLicence;
    const updatedBusinessLicence = this.siteFormStateService.businessLicenceFormState.json.businessLicence;
    const documentGuid = this.siteFormStateService.businessLicenceFormState.businessLicenceGuid.value;

    const request$ = concat(
      this.businessLicenceUpdates(
        siteId,
        currentBusinessLicence,
        updatedBusinessLicence,
        documentGuid
      ),
      this.siteResource.updateSite(this.siteFormStateService.json)
    );

    if (this.site.careSettingCode === CareSettingEnum.COMMUNITY_PHARMACY || this.siteFormStateService.businessLicenceFormState.pec.value) {
      return request$;
    }

    const data: DialogOptions = {
      title: 'Site ID',
      message: `Provide a Site ID if you have one. If you do not have one, or do not know what it is, you may continue.`,
      actionText: 'Continue'
    };

    return this.dialog.open(ConfirmDialogComponent, { data })
      .afterClosed()
      .pipe(
        exhaustMap((confirmation: boolean) => {
          if (confirmation) {
            return request$;
          }
          return EMPTY;
        })
      );
  }

  protected afterSubmitIsSuccessful(): void {
    const routePath = (this.isCompleted)
      ? SiteRoutes.SITE_REVIEW
      : SiteRoutes.HOURS_OPERATION;

    this.routeUtils.routeRelativeTo(routePath);
  }

  private getBusinessLicence(siteId: number): void {
    this.siteResource.getBusinessLicence(siteId)
      .subscribe((businessLicense: BusinessLicence) => {
        this.businessLicence = businessLicense ?? this.businessLicence;

        if (businessLicense) {
          const canDefer = this.canDefer();

          // Business licence may exist, but the deferred licence toggle may be
          // hidden based on care setting leaving the toggle undefined
          if (canDefer) {
            this.deferredLicenceToggle.checked = !!this.businessLicence.deferredLicenceReason;
          }

          this.updateBusLicAccess(this.deferredLicenceToggle.checked);
        }
      });
  }

  private updateBusLicAccess(toggleAccess: boolean): void {
    let enableOrDisable: 'enable' | 'disable';

    if (toggleAccess) {
      enableOrDisable = 'disable';
      this.updateBusLicValidations(
        [this.formState.deferredLicenceReason],
        [this.formState.businessLicenceExpiry]
      );
    } else {
      enableOrDisable = 'enable';
      this.updateBusLicValidations(
        [this.formState.businessLicenceExpiry],
        [this.formState.deferredLicenceReason]
      );
    }
  }

  private updateBusLicValidations(requiredControls: UntypedFormControl[], notRequiredControls: UntypedFormControl[]): void {
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
  ): Observable<BusinessLicence | BusinessLicenceDocument | void> {
    if (!currentBusinessLicence?.id) {
      // Create a business licence when none existed
      return this.siteResource.createBusinessLicence(siteId, updatedBusinessLicence, documentGuid);
    }

    updatedBusinessLicence.id = currentBusinessLicence.id;

    if (currentBusinessLicence.deferredLicenceReason !== updatedBusinessLicence.deferredLicenceReason) {
      // Remove an existing business licence document before updating
      // with a reason for deferment
      return (currentBusinessLicence?.businessLicenceDocument)
        ? this.siteResource.removeBusinessLicenceDocument(siteId, currentBusinessLicence.id)
          .pipe(
            tap(() => this.siteFormStateService.businessLicenceFormState.patchDocument(null)),
            exhaustMap(() => this.siteResource.updateBusinessLicence(siteId, updatedBusinessLicence))
          )
        // Create new document and update to remove the reason of deferment
        : (documentGuid)
          ? this.siteResource.createBusinessLicenceDocument(siteId, currentBusinessLicence.id, documentGuid)
            .pipe(
              this.afterCreateBusinessLicenseDocumentPipe(siteId, updatedBusinessLicence)
            )
          // Reason of deferment changed
          : this.siteResource.updateBusinessLicence(siteId, updatedBusinessLicence)
    }

    // Create a business licence document each time a file is uploaded, and
    // update the existing business licence
    return (documentGuid && documentGuid !== currentBusinessLicence.businessLicenceDocument?.documentGuid)
      ? this.siteResource.removeBusinessLicenceDocument(siteId, currentBusinessLicence.id)
        .pipe(
          exhaustMap(() => this.siteResource.createBusinessLicenceDocument(siteId, currentBusinessLicence.id, documentGuid)),
          this.afterCreateBusinessLicenseDocumentPipe(siteId, updatedBusinessLicence)
        )
      : this.siteResource.updateBusinessLicence(siteId, updatedBusinessLicence);
  }

  private afterCreateBusinessLicenseDocumentPipe(siteId: number, updatedBusinessLicence: BusinessLicence) {
    return pipe(
      tap((doc: BusinessLicenceDocument) => this.siteFormStateService.businessLicenceFormState.patchDocument(doc)),
      exhaustMap(() => this.siteResource.updateBusinessLicence(siteId, updatedBusinessLicence))
    )
  }
}
