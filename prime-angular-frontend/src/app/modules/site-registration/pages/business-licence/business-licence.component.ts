import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription } from 'rxjs';

import { RouteUtils } from '@registration/shared/classes/route-utils.class';
import { SiteRoutes } from '@registration/site-registration.routes';
import { SiteResource } from '@registration/shared/services/site-resource.service';
import { SiteService } from '@registration/shared/services/site.service';
import { BusinessLicenceDocument } from '@registration/shared/models/business-licence-document.model';
import { BaseDocument } from '@shared/components/document-upload/document-upload/document-upload.component';

@Component({
  selector: 'app-business-licence',
  templateUrl: './business-licence.component.html',
  styleUrls: ['./business-licence.component.scss']
})
export class BusinessLicenceComponent implements OnInit {
  public busy: Subscription;
  public title: string;
  public routeUtils: RouteUtils;
  public businessLicenceDocuments: BusinessLicenceDocument[];
  public hasNoBusinessLicenceError: boolean;
  public uploadedFile: boolean;
  public isCompleted: boolean;
  public SiteRoutes = SiteRoutes;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private siteService: SiteService,
    private siteResource: SiteResource,
  ) {
    this.title = 'Submit Your Business Licence';
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.MODULE_PATH);
    this.uploadedFile = false;
    this.businessLicenceDocuments = [];
  }

  public onSubmit() {
    if (this.businessLicenceDocuments.length || this.uploadedFile) {
      this.nextRoute();
    } else {
      this.hasNoBusinessLicenceError = true;
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
    const site = this.siteService.site;
    this.isCompleted = site?.completed;

    this.getBusinessLicences();
  }

  private getBusinessLicences() {
    const siteId = this.siteService.site.id;
    this.busy = this.siteResource.getBusinessLicences(siteId)
      .subscribe((businessLicenses: BusinessLicenceDocument[]) =>
        this.businessLicenceDocuments = businessLicenses
      );
  }
}
