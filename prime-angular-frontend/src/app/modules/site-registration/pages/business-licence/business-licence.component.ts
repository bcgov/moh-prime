import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup } from '@angular/forms';

import { Subscription } from 'rxjs';
import { map, tap } from 'rxjs/operators';

import { SiteResource } from '@core/resources/site-resource.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { OrganizationResource } from '@core/resources/organization-resource.service';
import { BaseDocument } from '@shared/components/document-upload/document-upload/document-upload.component';

import { RouteUtils } from '@registration/shared/classes/route-utils.class';
import { SiteRoutes } from '@registration/site-registration.routes';
import { Site } from '@registration/shared/models/site.model';
import { Organization } from '@registration/shared/models/organization.model';
import { BusinessLicenceDocument } from '@registration/shared/models/business-licence-document.model';
import { SiteService } from '@registration/shared/services/site.service';
import { SiteFormStateService } from '@registration/shared/services/site-form-state.service';
import { OrgBookResource } from '@registration/shared/services/org-book-resource.service';

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
  public businessLicenceDocuments: BusinessLicenceDocument[];
  public hasNoBusinessLicenceError: boolean;
  public uploadedFile: boolean;
  public doingBusinessAsNames: string[];
  public isCompleted: boolean;
  public SiteRoutes = SiteRoutes;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private siteService: SiteService,
    private siteFormStateService: SiteFormStateService,
    private organizationResource: OrganizationResource,
    private orgBookResource: OrgBookResource,
    private siteResource: SiteResource,
    private formUtilsService: FormUtilsService
  ) {
    this.title = 'Business Licence';
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.MODULE_PATH);
    this.uploadedFile = false;
    this.businessLicenceDocuments = [];
    this.doingBusinessAsNames = [];
  }

  public onSubmit() {
    const hasBusinessLicence = this.businessLicenceDocuments.length || this.uploadedFile;
    if (this.formUtilsService.checkValidity(this.form) && hasBusinessLicence) {
      const payload = this.siteFormStateService.json;
      this.siteResource
        .updateSite(payload)
        .subscribe(() => {
          this.form.markAsPristine();
          this.nextRoute();
        });
    } else {
      if (!hasBusinessLicence) {
        this.hasNoBusinessLicenceError = true;
      }
    }
  }

  public onUpload(event: BaseDocument) {
    const siteId = this.siteService.site.id;
    this.siteResource
      .createBusinessLicence(siteId, event.documentGuid, event.filename)
      .subscribe(() => {
        this.uploadedFile = true;
        this.hasNoBusinessLicenceError = false;
      });
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

    this.getBusinessLicences(site);
    this.getDoingBusinessAs(site);
  }

  private getBusinessLicences(site: Site) {
    this.busy = this.siteResource.getBusinessLicences(site.id)
      .subscribe((businessLicenses: BusinessLicenceDocument[]) =>
        this.businessLicenceDocuments = businessLicenses
      );
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
