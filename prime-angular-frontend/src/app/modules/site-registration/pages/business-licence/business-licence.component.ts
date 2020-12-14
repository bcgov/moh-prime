import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormControl, FormGroup, Validators } from '@angular/forms';

import { noop, Observable, of, Subscription } from 'rxjs';
import { exhaustMap, map, tap } from 'rxjs/operators';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { SiteResource } from '@core/resources/site-resource.service';
import { UtilsService } from '@core/services/utils.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { OrganizationResource } from '@core/resources/organization-resource.service';
import { BaseDocument, DocumentUploadComponent } from '@shared/components/document-upload/document-upload/document-upload.component';

import { SiteRoutes } from '@registration/site-registration.routes';
import { Site } from '@registration/shared/models/site.model';
import { Organization } from '@registration/shared/models/organization.model';
import { BusinessLicenceDocument } from '@registration/shared/models/business-licence-document.model';
import { SiteService } from '@registration/shared/services/site.service';
import { SiteFormStateService } from '@registration/shared/services/site-form-state.service';
import { OrgBookResource } from '@registration/shared/services/org-book-resource.service';
import { BusinessLicence } from '@registration/shared/models/business-licence.model';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';
import { MatSlideToggle, MatSlideToggleChange } from '@angular/material/slide-toggle';

@Component({
  selector: 'app-business-licence',
  templateUrl: './business-licence.component.html',
  styleUrls: ['./business-licence.component.scss']
})
export class BusinessLicenceComponent implements OnInit {
  public busy: Subscription;
  public form: FormGroup;
  public title: string;
  public routeUtils: RouteUtils;
  public businessLicence: BusinessLicence;
  public businessLicenceDocuments: BusinessLicenceDocument[];
  public uploadedFile: boolean;
  public hasNoBusinessLicenceError: boolean;
  public doingBusinessAsNames: string[];
  public isCompleted: boolean;
  public SiteRoutes = SiteRoutes;

  @ViewChild('deferredLicence') public deferredLicenceToggle: MatSlideToggle;
  @ViewChild('documentUpload') public documentUpload: DocumentUploadComponent;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private siteService: SiteService,
    private siteFormStateService: SiteFormStateService,
    private organizationResource: OrganizationResource,
    private orgBookResource: OrgBookResource,
    private siteResource: SiteResource,
    private formUtilsService: FormUtilsService,
    private utilsService: UtilsService
  ) {
    this.title = this.route.snapshot.data.title;
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.MODULE_PATH);
    this.uploadedFile = false;

    this.doingBusinessAsNames = [];

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

  public isCommPharm() {
    return this.siteService.site.careSettingCode === CareSettingEnum.COMMUNITY_PHARMACIST;
  }

  public onSubmit() {
    const siteId = this.route.snapshot.params.sid;
    let method$: Observable<any>;
    if (this.formUtilsService.checkValidity(this.form)
      && (this.uploadedFile || this.deferredLicenceToggle?.checked || this.businessLicence?.businessLicenceDocument)) {
      const payload = this.siteFormStateService.json;

      if (this.deferredLicenceToggle?.checked) {

        this.businessLicence.deferredLicenceReason = payload.deferredLicenceReason;
        if (this.businessLicence.id) {
          method$ = (this.businessLicence.businessLicenceDocument)
            // Returning: [Previously attached document] * [Document removed, Business Licence Updated] --> [...]
            ? this.siteResource.removeBusinessLicenceDocument(siteId).pipe(exhaustMap(() => this.siteResource.updateBusinessLicence(siteId, this.businessLicence)))
            // Returning: [] * [Business Licence Updated] --> [...]
            : this.siteResource.updateBusinessLicence(siteId, this.businessLicence);
        } else {
          // First time through: [] * [Business Licence Created] --> [...]
          method$ = this.siteResource.createBusinessLicence(siteId, this.businessLicence, null);
        }
      } else {
        const updateSite = this.siteResource.updateSite(payload);

        if (this.businessLicence.id) {
          method$ = (!this.uploadedFile)
            // Returning: [Previously attached document] * [Site Updated] --> [...]
            ? updateSite
            // Returning: [Previously attached document] * [Site Updated, New document uploaded] --> [...]
            : updateSite.pipe(exhaustMap(() =>
              this.siteResource.createBusinessLicenceDocument(siteId, payload.businessLicenceGuid)));
        } else {
          // First time through: [] * [Business Licence Created, New document uploaded] --> [...]
          method$ = updateSite.pipe(exhaustMap(() =>
            this.siteResource.createBusinessLicence(siteId, this.businessLicence, payload.businessLicenceGuid)));
        }

      }
      method$.subscribe(() => {
        // TODO should make this cleaner, but for now good enough
        // Remove the business licence GUID to prevent 404 already
        // submitted if resubmited in same session
        this.businessLicenceGuid.patchValue(null);
        this.form.markAsPristine();
        this.nextRoute();
      });
    } else if (!this.deferredLicenceToggle.checked) {
      this.hasNoBusinessLicenceError = true;
    }
  }

  public onUpload(document: BaseDocument) {
    this.businessLicenceGuid.patchValue(document.documentGuid);
    this.uploadedFile = true;
    this.hasNoBusinessLicenceError = false;
  }

  public onRemoveDocument(documentGuid: string) {
    this.businessLicenceGuid.patchValue(null);
  }

  public onBack() {
    this.routeUtils.routeRelativeTo(SiteRoutes.CARE_SETTING);
  }

  public nextRoute() {
    if (this.isCompleted) {
      this.routeUtils.routeRelativeTo(SiteRoutes.SITE_REVIEW);
    } else {
      this.routeUtils.routeRelativeTo(SiteRoutes.SITE_ADDRESS);
    }
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
    this.initForm();
  }

  private createFormInstance() {
    this.form = this.siteFormStateService.businessForm;
  }

  private initForm() {
    const site = this.siteService.site;
    this.isCompleted = site?.completed;
    this.siteFormStateService.setForm(site, true);
    this.form.markAsPristine();

    this.getBusinessLicence(site);
    this.getDoingBusinessAs(site);
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

  private getDoingBusinessAs(site: Site) {
    this.busy = this.organizationResource.getOrganizationById(site.organizationId)
      .pipe(
        map((organization: Organization) => organization.registrationId),
        this.orgBookResource.doingBusinessAsMap(),
        tap((doingBusinessAsNames: string[]) =>
          this.doingBusinessAsNames = doingBusinessAsNames
        )
      )
      .subscribe();
  }
}
