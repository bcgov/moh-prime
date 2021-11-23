import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder } from '@angular/forms';

import { noop, Observable, of } from 'rxjs';

import { SiteResource } from '@core/resources/site-resource.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { UtilsService } from '@core/services/utils.service';
import { NoContentResponse } from '@core/resources/abstract-resource';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { DocumentUploadComponent, BaseDocument } from '@shared/components/document-upload/document-upload/document-upload.component';

import { AbstractCommunitySiteRegistrationPage } from '@registration/shared/classes/abstract-community-site-registration-page.class';
import { BusinessLicenceDocument } from '@registration/shared/models/business-licence-document.model';
import { BusinessLicence } from '@registration/shared/models/business-licence.model';
import { Site } from '@registration/shared/models/site.model';
import { SiteFormStateService } from '@registration/shared/services/site-form-state.service';
import { SiteService } from '@registration/shared/services/site.service';
import { SiteRoutes } from '@registration/site-registration.routes';
import { BusinessLicenceRenewalPageFormState } from './business-licence-renewal-form-state.class';

@Component({
  selector: 'app-business-licence-renewal-page',
  templateUrl: './business-licence-renewal-page.component.html',
  styleUrls: ['./business-licence-renewal-page.component.scss']
})
export class BusinessLicenceRenewalPageComponent extends AbstractCommunitySiteRegistrationPage implements OnInit {
  public formState: BusinessLicenceRenewalPageFormState;
  public title: string;
  public routeUtils: RouteUtils;
  public businessLicence: BusinessLicence;
  public businessLicenceDocuments: BusinessLicenceDocument[];
  public uploadedFile: boolean;
  public hasNoBusinessLicenceError: boolean;
  public isCompleted: boolean;
  public SiteRoutes = SiteRoutes;
  public site: Site;

  @ViewChild('documentUpload') public documentUpload: DocumentUploadComponent;

  constructor(
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService,
    protected siteService: SiteService,
    protected siteFormStateService: SiteFormStateService,
    protected siteResource: SiteResource,
    private fb: FormBuilder,
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

  public onUpload(document: BaseDocument): void {
    this.formState.businessLicenceGuid.patchValue(document.documentGuid);
    this.uploadedFile = true;
    this.hasNoBusinessLicenceError = false;
  }

  public onRemoveDocument(_: string): void {
    this.formState.businessLicenceGuid.patchValue(null);
    this.uploadedFile = false;
  }

  public downloadBusinessLicence(event: Event): void {
    event.preventDefault();
    this.siteResource.getBusinessLicenceDocumentToken(this.siteService.site.id, this.siteService.site.businessLicence.id)
      .subscribe((token: string) => this.utilsService.downloadToken(token));
  }

  public onBack(): void {
    this.routeUtils.routeRelativeTo(SiteRoutes.SITE_REVIEW);
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.patchForm();
    this.initForm();
  }

  protected createFormInstance(): void {
    // TODO refactor business licence into a single page, or inverse these pages so 
    //      the default is the renewal page, and the override is the initial submission
    // Create out of band form state for renewals to be 
    // transferred to the form state service on submit
    this.formState = new BusinessLicenceRenewalPageFormState(this.fb);
  }

  protected patchForm(): void {
    const site = this.siteService.site;
    this.isCompleted = site?.completed;
  }

  protected initForm(): void {
    this.site = this.siteService.site;
    this.getBusinessLicence(this.site);
  }

  protected additionalValidityChecks(): boolean {
    return this.uploadedFile;
  }

  protected onSubmitFormIsValid(): void {
    this.hasNoBusinessLicenceError = false;
  }

  protected onSubmitFormIsInvalid(): void {
    if (!this.uploadedFile) {
      this.hasNoBusinessLicenceError = true;
    }
  }

  protected performSubmission(): Observable<void> {
    // Transfer business licence renewal updates
    // to the form state service for submission
    const { businessLicenceGuid, businessLicenceExpiry } = this.siteFormStateService.businessLicenceFormState;
    businessLicenceGuid.patchValue(this.formState.businessLicenceGuid.value);
    businessLicenceExpiry.patchValue(this.formState.businessLicenceExpiry.value);

    // Updates only occur on submission after an
    // initial submission has been performed
    return of(noop()).pipe(NoContentResponse);
  }

  protected afterSubmitIsSuccessful(): void {
    const routePath = SiteRoutes.SITE_REVIEW;

    this.routeUtils.routeRelativeTo(routePath);
  }

  private getBusinessLicence(site: Site): void {
    this.siteResource.getBusinessLicence(site.id)
      .subscribe((businessLicense: BusinessLicence) => {
        this.businessLicence = businessLicense ?? this.businessLicence;
      });
  }
}
