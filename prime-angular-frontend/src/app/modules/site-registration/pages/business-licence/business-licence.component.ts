import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription } from 'rxjs';

import { RouteUtils } from '@registration/shared/classes/route-utils.class';
import { SiteRoutes } from '@registration/site-registration.routes';
import { SiteResource } from '@registration/shared/services/site-resource.service';
import { SiteService } from '@registration/shared/services/site.service';
import { BusinessLicence } from '@registration/shared/models/business-licence.model';
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
  public businessLicences: BusinessLicence[];
  public isCompleted: boolean;
  public SiteRoutes = SiteRoutes;
  public hasNoLicenceError: boolean;
  public uploadedFile: boolean;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private siteService: SiteService,
    private siteResource: SiteResource,
  ) {
    this.title = 'Submit Your Business Licence';
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.MODULE_PATH);
  }

  public onSubmit() {
    // TODO validations temporarily turned off
    // if (this.siteService.site.businessLicences.length > 0 || this.uploadedFile) {
    this.nextRoute();
    // } else {
    //   this.hasNoLicenceError = true;
    // }
  }

  public onUpload(event: BaseDocument) {
    const siteId = this.siteService.site.id;
    this.siteResource
      .createBusinessLicence(siteId, event.documentGuid, event.filename).subscribe(() => {
        this.uploadedFile = true;
        this.hasNoLicenceError = false;
      });
  }

  public onBack() {
    this.routeUtils.routeRelativeTo(SiteRoutes.SITE_ADDRESS);
  }

  public nextRoute() {
    if (this.isCompleted) {
      this.routeUtils.routeRelativeTo(SiteRoutes.SITE_REVIEW);
    } else {
      this.routeUtils.routeRelativeTo(SiteRoutes.HOURS_OPERATION);
    }
  }

  public ngOnInit() {
    const site = this.siteService.site;
    this.isCompleted = site?.completed;

    this.uploadedFile = false;
  }

  private getBusinessLicences() {
    const siteId = this.siteService.site.id;
    return this.siteResource.getBusinessLicences(siteId)
      .subscribe((businessLicenses: BusinessLicence[]) =>
        this.businessLicences = businessLicenses
      );
  }
}
